namespace rss_api.Models.Business;

public class RssBusinessElements
{
	public Guid Id { get; set; }
	public string Tag { get; set; }
	public List<RssBusiness> RssBusinessItems { get; set; } = new ();
	public DateTime CreationDate { get; set; }
}