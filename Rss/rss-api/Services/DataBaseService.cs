using Mapster;
using Microsoft.EntityFrameworkCore;
using rss_api.Contexts;
using rss_api.Models.Business;
using rss_api.Models.Dal;
using Serilog;

namespace rss_api.Services;

public class DataBaseService(RssDbContext rssDbContext) : IDataBaseService
{
	public async Task StoreDataToDatabaseAsync(RssBusinessElements rssBusinessRange, CancellationToken cancellationToken)
	{
		try
		{
			if (rssBusinessRange != null)
			{
				var collectionForSave = rssBusinessRange.Adapt<RssDalElements>();

				await rssDbContext.RssElements.AddAsync(collectionForSave, cancellationToken);

				await rssDbContext.SaveChangesAsync(cancellationToken);
			}
		}
		catch (Exception e)
		{
			Log.Error(e.Message);
			throw;
		}
	}

	public async Task<RssBusinessElements> GetAllAsync(string tag, CancellationToken cancellationToken)
	{
		try
		{
			var collectionByTag = await rssDbContext.RssElements
				.AsNoTracking()
				.Include(x => x.RssDalItems)
				.Where(x=>x.Tag == tag)
				.FirstOrDefaultAsync(cancellationToken);

			var returnCollection = collectionByTag.Adapt<RssBusinessElements>();

			return returnCollection;
		}
		catch (Exception e)
		{
			Log.Error(e.Message);
			throw;
		}
	}

	public async Task<RssBusinessElements> GetByFilterAsync(string headerFilter, string bodyFilter, string key, CancellationToken cancellationToken)
	{
		try
		{
			var filteredByTag = await rssDbContext.RssElements
				.AsNoTracking()
				.Include(elements => elements.RssDalItems)
				.Select(dalElement => new RssDalElements
				{
					Id = dalElement.Id,
					Tag = dalElement.Tag,
					RssDalItems = dalElement.RssDalItems
						.Where(item =>
							(string.IsNullOrEmpty(headerFilter) || item.Header.Contains(headerFilter)) &&
							(string.IsNullOrEmpty(bodyFilter) || item.Description.Contains(bodyFilter)))
						.ToList(),
					CreationDate = dalElement.CreationDate
				})
				.Where(dtoElement => dtoElement.RssDalItems.Any() && dtoElement.Tag == key)
				.FirstOrDefaultAsync(cancellationToken: cancellationToken);

			var mappedResult = filteredByTag.Adapt<RssBusinessElements>();

			return mappedResult;
		}
		catch (Exception e)
		{
			Log.Information(e.Message);
			throw;
		}
	}
	
	public async Task<bool> AnyRecordsAsync(string tag, CancellationToken cancellationToken)
	{
		try
		{
			return await rssDbContext.RssElements
				.AsNoTracking()
				.AnyAsync(elements => elements.Tag == tag && elements.RssDalItems.Any(), cancellationToken);
		}
		catch (Exception e)
		{
			Log.Information(e.Message);
			throw;
		}
	}
}