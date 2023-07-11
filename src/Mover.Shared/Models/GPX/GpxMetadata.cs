namespace Mover.Shared.Models.GPX;

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.topografix.com/GPX/1/1")]
public partial class GpxMetadata
{

    private string nameField;

    private string descField;

    private GpxMetadataAuthor authorField;

    private GpxMetadataCopyright copyrightField;

    private GpxMetadataLink[] linkField;

    private DateTime timeField;

    private string keywordsField;

    private GpxMetadataBounds boundsField;

    private object extensionsField;

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
    public string desc
    {
        get
        {
            return this.descField;
        }
        set
        {
            this.descField = value;
        }
    }

    /// <remarks/>
    public GpxMetadataAuthor author
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

    /// <remarks/>
    public GpxMetadataCopyright copyright
    {
        get
        {
            return this.copyrightField;
        }
        set
        {
            this.copyrightField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElement("link")]
    public GpxMetadataLink[] link
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

    /// <remarks/>
    public DateTime time
    {
        get
        {
            return this.timeField;
        }
        set
        {
            this.timeField = value;
        }
    }

    /// <remarks/>
    public string keywords
    {
        get
        {
            return this.keywordsField;
        }
        set
        {
            this.keywordsField = value;
        }
    }

    /// <remarks/>
    public GpxMetadataBounds bounds
    {
        get
        {
            return this.boundsField;
        }
        set
        {
            this.boundsField = value;
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
