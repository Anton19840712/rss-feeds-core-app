using System.Xml.Serialization;

namespace rss_api.Models.PresentationXml;

public class ItunesOwner
{
    [XmlElement("itunes:name")]
    public string Name { get; set; }

    [XmlElement("itunes:email")]
    public string Email { get; set; }
}