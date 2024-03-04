namespace Mover.Shared.Models.GPX;

[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.topografix.com/GPX/1/1")]
public class GpxRte
{
    [System.Xml.Serialization.XmlElement("name")]
    public string? Name { get; set; }

    [System.Xml.Serialization.XmlElement("cmt")]
    public string? Cmt { get; set; }

    [System.Xml.Serialization.XmlElement("desc")]
    public string? Desc { get; set; }

    [System.Xml.Serialization.XmlElement("src")]
    public string? Src { get; set; }

    [System.Xml.Serialization.XmlElement("link")]
    public GpxLink[]? Link { get; set; }

    [System.Xml.Serialization.XmlElement("number")]
    public int? Number { get; set; }

    [System.Xml.Serialization.XmlElement("type")]
    public string? Type { get; set; }

    [System.Xml.Serialization.XmlElement("extensions")]
    public object? Extensions { get; set; }

    [System.Xml.Serialization.XmlElement("rtept")]
    public GpxPoint[]? Rtept { get; set; }
}
