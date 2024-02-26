using System.Xml.Serialization;

namespace rss_api.Models.PresentationXml;

public class Enclosure
{
    [XmlAttribute("url")]
    public string Url { get; set; }

    [XmlAttribute("length")]
    public string Length { get; set; }

    [XmlAttribute("type")]
    public string Type { get; set; }
}
