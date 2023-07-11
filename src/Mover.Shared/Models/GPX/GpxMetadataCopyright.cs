namespace Mover.Shared.Models.GPX;

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.topografix.com/GPX/1/1")]
public partial class GpxMetadataCopyright
{

    private ushort yearField;

    private string licenseField;

    private string authorField;

    /// <remarks/>
    public ushort year
    {
        get
        {
            return this.yearField;
        }
        set
        {
            this.yearField = value;
        }
    }

    /// <remarks/>
    public string license
    {
        get
        {
            return this.licenseField;
        }
        set
        {
            this.licenseField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttribute()]
    public string author
    {
        get
        {
            return this.authorField;
        }
        set
        {
            this.authorField = value;
        }
    }
}
