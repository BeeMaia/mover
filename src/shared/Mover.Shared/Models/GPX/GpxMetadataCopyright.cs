namespace Mover.Shared.Models.GPX;

[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.topografix.com/GPX/1/1")]
public class GpxMetadataCopyright
{
    [System.Xml.Serialization.XmlElement("year")]
    public ushort Year { get; set; }

    [System.Xml.Serialization.XmlElement("license")]
    public string? License { get; set; }

    [System.Xml.Serialization.XmlAttribute("author")]
    public string? Author { get; set; }
}
