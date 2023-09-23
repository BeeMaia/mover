namespace Mover.Shared.Models.GPX;

[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.topografix.com/GPX/1/1")]
public class GpxTrkTrkseg
{
    [System.Xml.Serialization.XmlElement("trkpt")]
    public GpxPoint[]? Trkpt { get; set; }

    [System.Xml.Serialization.XmlElement("extensions")]
    public object? Extensions { get; set; }
}
