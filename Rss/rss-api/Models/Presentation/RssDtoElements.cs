namespace rss_api.Models.Presentation;

public class RssDtoElements
{
	public Guid Id { get; set; }
	public string Tag { get; set; }
	public List<RssDto> RssDtoItems { get; set; } = new ();
	public DateTime CreationDate { get; set; }
}