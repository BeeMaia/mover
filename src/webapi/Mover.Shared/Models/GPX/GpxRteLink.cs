namespace Mover.Shared.Models.GPX;

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.topografix.com/GPX/1/1")]
public partial class GpxRteLink
{

    private string textField;

    private string typeField;

    private string hrefField;

    /// <remarks/>
    public string text
    {
        get
        {
            return this.textField;
        }
        set
        {
            this.textField = value;
        }
    }

    /// <remarks/>
    public string type
    {
        get
        {
            return this.typeField;
        }
        set
        {
            this.typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttribute()]
    public string href
    {
        get
        {
            return this.hrefField;
        }
        set
        {
            this.hrefField = value;
        }
    }
}
