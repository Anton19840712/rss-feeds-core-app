using Serilog;
using System.Xml.Serialization;
using System.Xml;
using rss_api.Models.Presentation;
using rss_api.Models.PresentationXml;
using Guid = System.Guid;

namespace rss_api.ControllerHandlers;

public class HttpService(IHttpClientFactory httpClientFactory) : IHttpService
{
    public async Task<RssDtoElements> GetNewsByHttpAsync(string source, CancellationToken cancellationToken)
    {
        var url = GetFeedUrlBySource(source);

        using var client = httpClientFactory.CreateClient();

        try
        {
            var xmlContent = await client.GetStringAsync(url, cancellationToken);
            using var stringReader = new StringReader(xmlContent);
            var serializer = new XmlSerializer(typeof(Rss));
            using var xmlReader = XmlReader.Create(stringReader);
            var deserializedData = (Rss)serializer.Deserialize(xmlReader);

            var returnModel = new RssDtoElements();

            returnModel.Id = Guid.NewGuid();
            returnModel.Tag = source;
            returnModel.CreationDate = DateTime.UtcNow;

			if (deserializedData!=null)
            {
				var items = deserializedData!.Channel.Items;

				foreach (var item in items)
				{
					var addItem = new RssDto();

					addItem.Id = Guid.NewGuid();
					addItem.Tag = source;
					addItem.Header = item.Title;
					addItem.Description = item.Description;
                    addItem.CreationDate = DateTime.UtcNow;

					returnModel.RssDtoItems.Add(addItem);
				}
			}

			return returnModel;
        }
        catch (HttpRequestException ex)
        {
            Log.Error($"Ошибка при выполнении HTTP-запроса: {ex.Message}");
        }

        return new RssDtoElements();
    }

    private string GetFeedUrlBySource(string source)
    {
        return source.ToLower() switch
        {
            "1" => "https://feeds.megaphone.fm/newheights",
            "2" => "https://podcastfeeds.nbcnews.com/HL4TzgYC",
            _ => null
        };
    }
}