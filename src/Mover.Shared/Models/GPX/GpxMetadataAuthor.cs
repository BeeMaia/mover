namespace Mover.Shared.Models.GPX;

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.topografix.com/GPX/1/1")]
public partial class GpxMetadataAuthor
{

    private string nameField;

    private GpxMetadataAuthorEmail emailField;

    private GpxMetadataAuthorLink linkField;

    /// <remarks/>
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    public GpxMetadataAuthorEmail email
    {
        get
        {
            return this.emailField;
        }
        set
        {
            this.emailField = value;
        }
    }

    /// <remarks/>
    public GpxMetadataAuthorLink link
    {
        get
        {
            return this.linkField;
        }
        set
        {
            this.linkField = value;
        }
    }
}
