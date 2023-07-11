namespace Mover.Shared.Models.GPX;

/// <remarks/>
[Serializable()]
public partial class TrackPointExtension
{

    private decimal atempField;

    private decimal speedField;

    private short hrField;

    private short cadField;

    /// <remarks/>
    public decimal atemp
    {
        get
        {
            return this.atempField;
        }
        set
        {
            this.atempField = value;
        }
    }

    public decimal speed
    {
        get
        {
            return this.speedField;
        }
        set
        {
            this.speedField = value;
        }
    }

    /// <remarks/>
    public short hr
    {
        get
        {
            return this.hrField;
        }
        set
        {
            this.hrField = value;
        }
    }

    /// <remarks/>
    public short cad
    {
        get
        {
            return this.cadField;
        }
        set
        {
            this.cadField = value;
        }
    }
}
