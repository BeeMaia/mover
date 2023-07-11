namespace Mover.Shared.Models.GPX;

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.topografix.com/GPX/1/1")]
[System.Xml.Serialization.XmlRoot(Namespace = "http://www.topografix.com/GPX/1/1", IsNullable = false)]
public partial class Gpx
{

    private GpxMetadata metadataField;

    private GpxWpt[] wptField;

    private GpxRte[] rteField;

    private GpxTrk[] trkField;

    private object extensionsField;

    private decimal versionField;

    private string creatorField;

    /// <remarks/>
    public GpxMetadata metadata
    {
        get
        {
            return this.metadataField;
        }
        set
        {
            this.metadataField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElement("wpt")]
    public GpxWpt[] wpt
    {
        get
        {
            return this.wptField;
        }
        set
        {
            this.wptField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElement("rte")]
    public GpxRte[] rte
    {
        get
        {
            return this.rteField;
        }
        set
        {
            this.rteField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElement("trk")]
    public GpxTrk[] trk
    {
        get
        {
            return this.trkField;
        }
        set
        {
            this.trkField = value;
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

    /// <remarks/>
    [System.Xml.Serialization.XmlAttribute()]
    public decimal version
    {
        get
        {
            return this.versionField;
        }
        set
        {
            this.versionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttribute()]
    public string creator
    {
        get
        {
            return this.creatorField;
        }
        set
        {
            this.creatorField = value;
        }
    }
}
