using System.Xml.Serialization;

namespace rss_api.Models.PresentationXml;

public class Guid
{
    [XmlText]
    public string Value { get; set; }

    [XmlAttribute("isPermaLink")]
    public bool IsPermaLink { get; set; }
}