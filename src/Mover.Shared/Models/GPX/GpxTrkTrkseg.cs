namespace Mover.Shared.Models.GPX;

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.topografix.com/GPX/1/1")]
public partial class GpxTrkTrkseg
{

    private GpxTrkTrksegTrkpt[] trkptField;

    private object extensionsField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElement("trkpt")]
    public GpxTrkTrksegTrkpt[] trkpt
    {
        get
        {
            return this.trkptField;
        }
        set
        {
            this.trkptField = value;
        }
    }

    /// <remarks/>
    public object extensions
    {
        get
        {
            return this.extensionsField;
        }
        set
        {
            this.extensionsField = value;
        }
    }
}
