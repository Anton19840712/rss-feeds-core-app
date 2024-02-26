using System.Xml.Serialization;

namespace rss_api.Models.PresentationXml;

public class AtomLink
{
    [XmlAttribute("href")]
    public string Href { get; set; }

    [XmlAttribute("rel")]
    public string Rel { get; set; }

    [XmlAttribute("type")]
    public string Type { get; set; }
}