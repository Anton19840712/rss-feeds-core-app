using System.Xml.Serialization;

namespace rss_api.Models.PresentationXml;

public class Item
{
    [XmlElement("title")]
    public string Title { get; set; }

    [XmlElement("link")]
    public string Link { get; set; }

    [XmlElement("description")]
    public string Description { get; set; }

    [XmlElement("pubDate")]
    public string PubDate { get; set; }

    [XmlElement("itunes:title")]
    public string ItunesTitle { get; set; }

    [XmlElement("itunes:episodeType")]
    public string ItunesEpisodeType { get; set; }

    [XmlElement("itunes:season")]
    public string ItunesSeason { get; set; }

    [XmlElement("itunes:episode")]
    public string ItunesEpisode { get; set; }

    [XmlElement("itunes:author")]
    public string ItunesAuthor { get; set; }

    [XmlElement("itunes:subtitle")]
    public string ItunesSubtitle { get; set; }

    [XmlElement("itunes:summary")]
    public string ItunesSummary { get; set; }

    [XmlElement("content:encoded")]
    public string ItemContentEncoded { get; set; }

    [XmlElement("itunes:duration")]
    public string ItunesDuration { get; set; }

    [XmlElement("itunes:explicit")]
    public string ItunesExplicit { get; set; }

    [XmlElement("guid")]
    public Guid Guid { get; set; }

    [XmlElement("enclosure")]
    public Enclosure Enclosure { get; set; }
}