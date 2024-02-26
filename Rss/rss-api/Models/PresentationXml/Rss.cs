using System.Xml.Serialization;

namespace rss_api.Models.PresentationXml;

[XmlRoot("rss")]
public class Rss
{
    [XmlAttribute("version")]
    public string Version { get; set; }

    [XmlElement("channel")]
    public Channel Channel { get; set; }
}