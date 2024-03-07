namespace Mover.Shared.Models.GPX;

[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.topografix.com/GPX/1/1")]
public class GpxLink
{
    [System.Xml.Serialization.XmlElement("text")]
    public string? Text { get; set; }

    [System.Xml.Serialization.XmlElement("type")]
    public string? Type { get; set; }

    [System.Xml.Serialization.XmlAttribute("href")]
    public string? Href { get; set; }
}
