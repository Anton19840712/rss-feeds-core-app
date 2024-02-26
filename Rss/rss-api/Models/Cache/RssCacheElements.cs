namespace rss_api.Models.Cache;

public class RssCacheElements
{
	public Guid Id { get; set; }
	public string Tag { get; set; }
	public List<RssCache> RssCacheItems { get; set; } = new();
	public DateTime CreationDate { get; set; }
}