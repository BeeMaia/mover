using System.Xml.Serialization;

namespace Mover.Shared.Models.GPX;

[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://www.topografix.com/GPX/1/1")]
[XmlInclude(typeof(GpxTrkTrkptExtensions))]
public class GpxTrkTrkptExtensions
{
    [XmlElement("trkptextension")]
    public TrackPointExtension? TrackPointExtension { get; set; }
}