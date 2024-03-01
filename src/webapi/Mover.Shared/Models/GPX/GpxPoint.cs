namespace Mover.Shared.Models.GPX;

[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.topografix.com/GPX/1/1")]
public class GpxPoint
{
    [System.Xml.Serialization.XmlElement("ele")]
    public decimal Ele { get; set; }

    [System.Xml.Serialization.XmlElement("time")]
    public DateTime Time { get; set; }

    [System.Xml.Serialization.XmlElement("magvar")]
    public decimal Magvar { get; set; }

    [System.Xml.Serialization.XmlElement("geoidheight")]
    public decimal Geoidheight { get; set; }

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

    [System.Xml.Serialization.XmlElement("sym")]
    public string? Sym { get; set; }

    [System.Xml.Serialization.XmlElement("type")]
    public string? Type { get; set; }

    [System.Xml.Serialization.XmlElement("fix")]
    public string? Fix { get; set; }

    [System.Xml.Serialization.XmlElement("sat", DataType = "integer")]
    public int Sat { get; set; }

    [System.Xml.Serialization.XmlElement("hdop")]
    public decimal Hdop { get; set; }

    [System.Xml.Serialization.XmlElement("vdop")]
    public decimal Vdop { get; set; }

    [System.Xml.Serialization.XmlElement("pdop")]
    public decimal Pdop { get; set; }

    [System.Xml.Serialization.XmlElement("ageofdgpsdata")]
    public decimal Ageofdgpsdata { get; set; }

    [System.Xml.Serialization.XmlElement("dgpsid")]
    public ushort Dgpsid { get; set; }

    [System.Xml.Serialization.XmlElement("extensions")]
    public object? Extensions { get; set; }

    [System.Xml.Serialization.XmlAttribute("lat")]
    public decimal Lat { get; set; }

    [System.Xml.Serialization.XmlAttribute("lon")]
    public decimal Lon { get; set; }
}
