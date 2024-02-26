using System.Xml.Serialization;

namespace rss_api.Models.PresentationXml;

public class Channel
{
    [XmlElement("atom:link")]
    public AtomLink AtomLink { get; set; }

    [XmlElement("title")]
    public string Title { get; set; }

    [XmlElement("link")]
    public string Link { get; set; }

    [XmlElement("language")]
    public string Language { get; set; }

    [XmlElement("copyright")]
    public string Copyright { get; set; }

    [XmlElement("description")]
    public string Description { get; set; }

    [XmlElement("image")]
    public Image Image { get; set; }

    [XmlElement("itunes:explicit")]
    public string ItunesExplicit { get; set; }

    [XmlElement("itunes:type")]
    public string ItunesType { get; set; }

    [XmlElement("itunes:subtitle")]
    public string ItunesSubtitle { get; set; }

    [XmlElement("itunes:author")]
    public string ItunesAuthor { get; set; }

    [XmlElement("itunes:summary")]
    public string ItunesSummary { get; set; }

    [XmlElement("content:encoded")]
    public string ContentEncoded { get; set; }

    [XmlElement("itunes:owner")]
    public ItunesOwner ItunesOwner { get; set; }

    [XmlElement("itunes:image")]
    public ItunesImage ItunesImage { get; set; }

    [XmlElement("itunes:category")]
    public List<ItunesCategory> ItunesCategories { get; set; }

    [XmlElement("item")]
    public List<Item> Items { get; set; }
}