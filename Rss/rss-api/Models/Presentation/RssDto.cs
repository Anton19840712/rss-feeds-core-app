namespace rss_api.Models.Presentation;

public class RssDto
{
	public Guid Id { get; set; }
	public string Tag { get; set; }
	public string Header { get; set; }
	public string Description { get; set; }
	public DateTime CreationDate { get; set; }
}