using rss_api.Models.Business;

namespace rss_api.Services;

public interface IDataBaseService
{
	Task StoreDataToDatabaseAsync(RssBusinessElements rssBusinessRange, CancellationToken cancellationToken);
	Task<RssBusinessElements> GetAllAsync(string tag, CancellationToken cancellationToken);
	Task<RssBusinessElements> GetByFilterAsync(string headerFilter, string bodyFilter, string key, CancellationToken cancellationToken);
	Task<bool> AnyRecordsAsync(string tag, CancellationToken cancellationToken);
}
