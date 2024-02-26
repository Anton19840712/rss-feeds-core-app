using Hangfire;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using rss_api.ControllerHandlers;
using rss_api.Models.Business;
using rss_api.Models.Cache;
using rss_api.Models.Presentation;
using rss_api.Services;

namespace rss_api.Controllers;

[ApiController]
[Route("[controller]")]
public class RssController(
	IHttpService httpParseService,
	ICacheService cacheRedisService,
	IDataBaseService dataBaseService,
	IHangFireService removeService) : ControllerBase
{
	[HttpGet("get-news")]
	public async Task<ActionResult> GetByAggregateAsync(
		string sourceKeyOne = null,
		string sourceKeyTwo = null,
		string headerFilter = null,
		string bodyFilter = null)
	{
		var cancellationToken = new CancellationToken();
		var source = new List<string> { sourceKeyOne, sourceKeyTwo };
		var filters = new List<string>() { headerFilter, bodyFilter };

		var collectionDtoReturn = new List<RssDtoElements>();

		if (!source.Any())
		{
			return BadRequest("No keys for news aggregation obtained.");
		}

		// сначала идем смотреть в кэш...
		foreach (var tag in source)
		{
			var cacheData = cacheRedisService.GetData<RssCacheElements>(tag);

			if (cacheData != null)
			{
				var cacheToDto = cacheData.Adapt<RssDtoElements>();

				// фильтруем данные из кэша
				var filteredDtoElements = cacheToDto.RssDtoItems.Where(element =>
				{
					var headerMatch = string.IsNullOrEmpty(headerFilter) || element.Header.Contains(headerFilter);
					var bodyMatch = string.IsNullOrEmpty(bodyFilter) || element.Description.Contains(bodyFilter);

					return headerMatch && bodyMatch;
				}).ToList();

				cacheToDto.RssDtoItems = filteredDtoElements;

				collectionDtoReturn.Add(cacheToDto);
			}
			// если же в кеше нету по ключу, то смотрим в базу данных:
			else
			{
				var result = await dataBaseService.AnyRecordsAsync(tag, cancellationToken);

				if (result)
				{
					if (filters.Any())
					{
						var resultBusiness = await dataBaseService.GetByFilterAsync(headerFilter, bodyFilter, tag, cancellationToken);

						if (resultBusiness != null)
						{
							var mappedDtoResult = resultBusiness.Adapt<RssDtoElements>();
							collectionDtoReturn.Add(mappedDtoResult);
						}
					}
					else
					{
						var resultBusiness = await dataBaseService.GetAllAsync(tag, cancellationToken);

						if (resultBusiness != null)
						{
							var mappedDtoResult = resultBusiness.Adapt<RssDtoElements>();
							collectionDtoReturn.Add(mappedDtoResult);
						}
					}
				}
				else
				{
					var httpData = await httpParseService.GetNewsByHttpAsync(tag, cancellationToken);

					if (httpData.RssDtoItems.Any())
					{
						var mappedToCache = httpData.Adapt<RssCacheElements>();
						mappedToCache.RssCacheItems = httpData.RssDtoItems.Adapt<List<RssCache>>();

						var expiryTime = DateTimeOffset.Now.AddSeconds(30);
						cacheRedisService.SetData<RssCacheElements>($"{tag}", mappedToCache, expiryTime);

						var mappedBusinessToStore = httpData.Adapt<RssBusinessElements>();
						await dataBaseService.StoreDataToDatabaseAsync(mappedBusinessToStore, cancellationToken);

						collectionDtoReturn.Add(httpData);
					}
					else
					{
						return NotFound();
					}
				}
			}
		}

		var data = collectionDtoReturn
			.Where(x => x.RssDtoItems != null && x.RssDtoItems.Any(zx =>
				(string.IsNullOrEmpty(headerFilter) || zx.Header.Contains(headerFilter)) &&
				(string.IsNullOrEmpty(bodyFilter) || zx.Description.Contains(bodyFilter))))
			.Select(f => new RssDtoElements
			{
				Id = f.Id,
				Tag = f.Tag,
				CreationDate = f.CreationDate,
				RssDtoItems = f.RssDtoItems
					.Where(s =>
						(string.IsNullOrEmpty(headerFilter) || s.Header.Contains(headerFilter)) &&
						(string.IsNullOrEmpty(bodyFilter) || s.Description.Contains(bodyFilter)))
					.ToList()
			})
			.ToList();

		
		//ставим данные на автоматическое удаление из базы:
		RecurringJob.AddOrUpdate("kill-bill", () => removeService.DeleteObsoleteRecordsAsync(cancellationToken), "*/2 * * * *");

		return Ok(data);
	}
}