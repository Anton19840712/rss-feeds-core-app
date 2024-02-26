using rss_api.Contexts;
using Serilog;

namespace rss_api.Services;

public class HangFireService(RssDbContext rssDbContext) : IHangFireService
{
	public async Task DeleteObsoleteRecordsAsync(CancellationToken cancellationToken)
	{
		try
		{
			rssDbContext.RssElements.RemoveRange(rssDbContext.RssElements);
			await rssDbContext.SaveChangesAsync(cancellationToken);

			Log.Information("Obsolete data was deleted from PostgresSQL database successfully");
		}
		catch (Exception e)
		{
			Log.Error(e.Message);
			throw;
		}
	}
}