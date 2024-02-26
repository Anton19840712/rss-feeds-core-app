using System.Xml.Serialization;

namespace rss_api.Models.PresentationXml;

public class ItunesCategory
{
    [XmlAttribute("text")]
    public string Text { get; set; }

    [XmlElement("itunes:category")]
    public ItunesCategory SubCategory { get; set; }
}