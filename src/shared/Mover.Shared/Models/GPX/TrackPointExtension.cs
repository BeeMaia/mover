namespace Mover.Shared.Models.GPX;

[Serializable()]
public class TrackPointExtension
{
    [System.Xml.Serialization.XmlElement("atemp")]
    public decimal Temp { get; set; }

    [System.Xml.Serialization.XmlElement("speed")]
    public decimal Speed { get; set; }

    [System.Xml.Serialization.XmlElement("hr")]
    public short HeartRate { get; set; }

    [System.Xml.Serialization.XmlElement("cad")]
    public short Cadence { get; set; }
}
