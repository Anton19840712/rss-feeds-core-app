using rss_api.Models.Business;

namespace rss_api.Services.Entities;

public interface IPushService
{
	Task StoreDataToDatabaseAsync(RssBusinessElements rssBusinessRange, CancellationToken cancellationToken);
}