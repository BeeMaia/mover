namespace Mover.Shared.Models.GPX;

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.topografix.com/GPX/1/1")]
public partial class GpxMetadataAuthorEmail
{

    private string idField;

    private string domainField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttribute()]
    public string id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttribute()]
    public string domain
    {
        get
        {
            return this.domainField;
        }
        set
        {
            this.domainField = value;
        }
    }
}
