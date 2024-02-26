namespace rss_api.Services;

public interface IHangFireService
{
	Task DeleteObsoleteRecordsAsync(CancellationToken cancellationToken);
}