namespace Mover.Shared.Models.GPX;

[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.topografix.com/GPX/1/1")]
public class GpxMetadataBounds
{
    [System.Xml.Serialization.XmlAttribute("minlat")]
    public decimal Minlat { get; set; }

    [System.Xml.Serialization.XmlAttribute("minlon")]
    public decimal Minlon { get; set; }
    
    [System.Xml.Serialization.XmlAttribute("maxlat")]
    public decimal Maxlat { get; set; }
    
    [System.Xml.Serialization.XmlAttribute("maxlon")]
    public decimal Maxlon { get; set; }
}
