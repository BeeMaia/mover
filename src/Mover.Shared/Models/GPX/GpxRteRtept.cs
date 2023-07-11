namespace Mover.Shared.Models.GPX;

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.topografix.com/GPX/1/1")]
public partial class GpxRteRtept
{

    private decimal eleField;

    private DateTime timeField;

    private decimal magvarField;

    private decimal geoidheightField;

    private string nameField;

    private string cmtField;

    private string descField;

    private string srcField;

    private GpxRteRteptLink[] linkField;

    private string symField;

    private string typeField;

    private string fixField;

    private string satField;

    private decimal hdopField;

    private decimal vdopField;

    private decimal pdopField;

    private decimal ageofdgpsdataField;

    private ushort dgpsidField;

    private object extensionsField;

    private decimal latField;

    private decimal lonField;

    /// <remarks/>
    public decimal ele
    {
        get
        {
            return this.eleField;
        }
        set
        {
            this.eleField = value;
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
    public decimal magvar
    {
        get
        {
            return this.magvarField;
        }
        set
        {
            this.magvarField = value;
        }
    }

    /// <remarks/>
    public decimal geoidheight
    {
        get
        {
            return this.geoidheightField;
        }
        set
        {
            this.geoidheightField = value;
        }
    }

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
    public string cmt
    {
        get
        {
            return this.cmtField;
        }
        set
        {
            this.cmtField = value;
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
    public string src
    {
        get
        {
            return this.srcField;
        }
        set
        {
            this.srcField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElement("link")]
    public GpxRteRteptLink[] link
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
    public string sym
    {
        get
        {
            return this.symField;
        }
        set
        {
            this.symField = value;
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
    public string fix
    {
        get
        {
            return this.fixField;
        }
        set
        {
            this.fixField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElement(DataType = "integer")]
    public string sat
    {
        get
        {
            return this.satField;
        }
        set
        {
            this.satField = value;
        }
    }

    /// <remarks/>
    public decimal hdop
    {
        get
        {
            return this.hdopField;
        }
        set
        {
            this.hdopField = value;
        }
    }

    /// <remarks/>
    public decimal vdop
    {
        get
        {
            return this.vdopField;
        }
        set
        {
            this.vdopField = value;
        }
    }

    /// <remarks/>
    public decimal pdop
    {
        get
        {
            return this.pdopField;
        }
        set
        {
            this.pdopField = value;
        }
    }

    /// <remarks/>
    public decimal ageofdgpsdata
    {
        get
        {
            return this.ageofdgpsdataField;
        }
        set
        {
            this.ageofdgpsdataField = value;
        }
    }

    /// <remarks/>
    public ushort dgpsid
    {
        get
        {
            return this.dgpsidField;
        }
        set
        {
            this.dgpsidField = value;
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
    public decimal lat
    {
        get
        {
            return this.latField;
        }
        set
        {
            this.latField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttribute()]
    public decimal lon
    {
        get
        {
            return this.lonField;
        }
        set
        {
            this.lonField = value;
        }
    }
}
