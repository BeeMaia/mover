namespace Mover.Shared.Models.GPX;

[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.topografix.com/GPX/1/1")]
public class GpxMetadata
{
    [System.Xml.Serialization.XmlElement("name")]
    public string? Name { get; set; }

    [System.Xml.Serialization.XmlElement("desc")]
    public string? Desc { get; set; }

    [System.Xml.Serialization.XmlElement("author")]
    public GpxMetadataAuthor? Author { get; set; }

    [System.Xml.Serialization.XmlElement("copyright")]
    public GpxMetadataCopyright? Copyright { get; set; }

    [System.Xml.Serialization.XmlElement("link")]
    public GpxLink[]? Link { get; set; }

    [System.Xml.Serialization.XmlElement("time")]
    public DateTime Time { get; set; }

    [System.Xml.Serialization.XmlElement("keywords")]
    public string? Keywords { get; set; }

    [System.Xml.Serialization.XmlElement("bounds")]
    public GpxMetadataBounds? Bounds { get; set; }

    [System.Xml.Serialization.XmlElement("extensions")]
    public object? Extensions { get; set; }
}
