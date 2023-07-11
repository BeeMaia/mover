﻿namespace Mover.Shared.Models.GPX;

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.topografix.com/GPX/1/1")]
public partial class GpxTrk
{

    private string nameField;

    private string cmtField;

    private string descField;

    private string srcField;

    private GpxTrkLink[] linkField;

    private string numberField;

    private string typeField;

    private object extensionsField;

    private GpxTrkTrkseg[] trksegField;

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
    public GpxTrkLink[] link
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
    [System.Xml.Serialization.XmlElement(DataType = "integer")]
    public string number
    {
        get
        {
            return this.numberField;
        }
        set
        {
            this.numberField = value;
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
    [System.Xml.Serialization.XmlElement("trkseg")]
    public GpxTrkTrkseg[] trkseg
    {
        get
        {
            return this.trksegField;
        }
        set
        {
            this.trksegField = value;
        }
    }
}
