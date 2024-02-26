using NpgsqlTypes;

namespace rss_api.Models.Dal;

public class RssDal
{
	public Guid Id { get; set; }
	public Guid RssDalElementsId { get; set; }
	public string Tag { get; set; }
	public string Header { get; set; }
	public string Description { get; set; }
	public NpgsqlTsVector SearchVector { get; set; }
	public DateTime CreationDate { get; set; }
}