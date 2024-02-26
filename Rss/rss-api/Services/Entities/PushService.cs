using Mapster;
using rss_api.Contexts;
using rss_api.Models.Business;
using rss_api.Models.Dal;
using Serilog;

namespace rss_api.Services.Entities;

public class PushService(RssDbContext rssDbContext) : IPushService
{
	public async Task StoreDataToDatabaseAsync(RssBusinessElements rssBusinessRange,
		CancellationToken cancellationToken)
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
}