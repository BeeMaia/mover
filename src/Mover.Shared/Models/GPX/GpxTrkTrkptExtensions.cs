namespace Mover.Shared.Models.GPX;

[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.topografix.com/GPX/1/1")]
public partial class GpxTrkTrkptExtensions
{
    private TrackPointExtension? trackPointExtensionField;

    [System.Xml.Serialization.XmlElement("trkptextension")]
    public TrackPointExtension? TrackPointExtension
    {
        get
        {
            return this.trackPointExtensionField;
        }
        set
        {
            this.trackPointExtensionField = value;
        }
    }
}