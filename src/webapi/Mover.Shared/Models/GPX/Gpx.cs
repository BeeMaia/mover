namespace Mover.Shared.Models.GPX;

[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.topografix.com/GPX/1/1")]
[System.Xml.Serialization.XmlRoot(Namespace = "http://www.topografix.com/GPX/1/1", ElementName ="gpx", IsNullable = false)]
public class Gpx
{
    [System.Xml.Serialization.XmlElement("metadata")]
    public GpxMetadata? Metadata { get; set; }

    [System.Xml.Serialization.XmlElement("wpt")]
    public GpxPoint[]? Wpt { get; set; }

    [System.Xml.Serialization.XmlElement("rte")]
    public GpxRte[]? Rte { get; set; }

    [System.Xml.Serialization.XmlElement("trk")]
    public GpxTrk[]? Trk { get; set; }

    [System.Xml.Serialization.XmlElement("extensions")]
    public object? Extensions { get; set; }

    [System.Xml.Serialization.XmlAttribute("version")]
    public decimal Version { get;set; }

    [System.Xml.Serialization.XmlAttribute("creator")]
    public string? Creator { get; set; }
}
