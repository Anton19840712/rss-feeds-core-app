namespace rss_api.Services.Hangfire;

public interface IHangFireService
{
    Task DeleteObsoleteRecordsAsync(CancellationToken cancellationToken);
}