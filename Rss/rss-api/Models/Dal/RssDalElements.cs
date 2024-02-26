namespace rss_api.Models.Dal;

public class RssDalElements
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Tag { get; set; }
	public List<RssDal> RssDalItems { get; set; } = new ();
	public DateTime CreationDate { get; set; }
}