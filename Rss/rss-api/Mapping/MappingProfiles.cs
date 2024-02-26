using Mapster;
using rss_api.Models.Business;
using rss_api.Models.Cache;
using rss_api.Models.Dal;
using rss_api.Models.Presentation;

namespace rss_api.Mapping;

public static class MappingProfiles
{
	public static void AddMapster(this IServiceCollection services)
	{
		TypeAdapterConfig<RssDto, RssBusiness>.NewConfig()
			.Map(dest => dest.Id, src => src.Id)
			.Map(dest => dest.Tag, src => src.Tag)
			.Map(dest => dest.Header, src => src.Header)
			.Map(dest => dest.Description, src => src.Description)
			.Map(dest => dest.CreationDate, src => src.CreationDate)
			.TwoWays();

		TypeAdapterConfig<RssDtoElements, RssBusinessElements>.NewConfig()
			.Map(dest => dest.Id, src => src.Id)
			.Map(dest => dest.Tag, src => src.Tag)
			.Map(dest => dest.CreationDate, src => src.CreationDate)
			.Map(dest => dest.RssBusinessItems, src => src.RssDtoItems.Select(x=>x.Adapt<RssBusiness>()));

		TypeAdapterConfig<RssBusinessElements, RssDtoElements>.NewConfig()
			.Map(dest => dest.Id, src => src.Id)
			.Map(dest => dest.Tag, src => src.Tag)
			.Map(dest => dest.CreationDate, src => src.CreationDate)
			.Map(dest => dest.RssDtoItems, src => src.RssBusinessItems.Select(x => x.Adapt<RssDto>()));

		TypeAdapterConfig<RssBusiness, RssDal>.NewConfig()
			.Map(dest => dest.Id, src => src.Id)
			.Map(dest => dest.Tag, src => src.Tag)
			.Map(dest => dest.Header, src => src.Header)
			.Map(dest => dest.Description, src => src.Description)
			.Map(dest => dest.CreationDate, src => src.CreationDate)
			.TwoWays();

		TypeAdapterConfig<RssBusinessElements, RssDalElements>.NewConfig()
			.Map(dest => dest.Id, src => src.Id)
			.Map(dest => dest.Tag, src => src.Tag)
			.Map(dest => dest.CreationDate, src => src.CreationDate)
			.Map(dest => dest.RssDalItems, src => src.RssBusinessItems.Select(x => x.Adapt<RssDal>()));


		TypeAdapterConfig<RssDalElements, RssBusinessElements>.NewConfig()
			.Map(dest => dest.Id, src => src.Id)
			.Map(dest => dest.Tag, src => src.Tag)
			.Map(dest => dest.CreationDate, src => src.CreationDate)
			.Map(dest => dest.RssBusinessItems, src => src.RssDalItems.Select(x => x.Adapt<RssBusiness>()));


		TypeAdapterConfig<RssCache, RssDto>.NewConfig()
			.Map(dest => dest.Id, src => src.Id)
			.Map(dest => dest.Tag, src => src.Tag)
			.Map(dest => dest.Header, src => src.Header)
			.Map(dest => dest.Description, src => src.Description)
			.Map(dest => dest.CreationDate, src => src.CreationDate)
			.TwoWays();

		TypeAdapterConfig<RssCacheElements, RssDtoElements>.NewConfig()
			.Map(dest => dest.Id, src => src.Id)
			.Map(dest => dest.Tag, src => src.Tag)
			.Map(dest => dest.CreationDate, src => src.CreationDate)
			.Map(dest => dest.RssDtoItems, src => src.RssCacheItems.Select(x => x.Adapt<RssDto>()));
	}
}