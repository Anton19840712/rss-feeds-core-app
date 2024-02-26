using rss_api.Models.Presentation;

namespace rss_api.ControllerHandlers;

public interface IHttpService
{
	Task<RssDtoElements> GetNewsByHttpAsync(string source, CancellationToken cancellationToken);
}