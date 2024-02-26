using System.Xml.Serialization;

namespace rss_api.Models.PresentationXml;

public class ItunesImage
{
    [XmlAttribute("href")]
    public string Href { get; set; }
}