namespace Mover.Shared.Models.GPX;

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.topografix.com/GPX/1/1")]
public partial class GpxMetadataBounds
{

    private decimal minlatField;

    private decimal minlonField;

    private decimal maxlatField;

    private decimal maxlonField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttribute()]
    public decimal minlat
    {
        get
        {
            return this.minlatField;
        }
        set
        {
            this.minlatField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttribute()]
    public decimal minlon
    {
        get
        {
            return this.minlonField;
        }
        set
        {
            this.minlonField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttribute()]
    public decimal maxlat
    {
        get
        {
            return this.maxlatField;
        }
        set
        {
            this.maxlatField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttribute()]
    public decimal maxlon
    {
        get
        {
            return this.maxlonField;
        }
        set
        {
            this.maxlonField = value;
        }
    }
}
