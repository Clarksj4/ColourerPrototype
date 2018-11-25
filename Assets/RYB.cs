using System;

/// <summary>
/// RYB structure.
/// </summary>
public struct RYB
{
    /// <summary>
    /// Gets an empty RYB structure;
    /// </summary>
    public static readonly RYB Empty = new RYB();

    private int red;
    private int yellow;
    private int blue;

    public static bool operator ==(RYB item1, RYB item2)
    {
        return (
            item1.Red == item2.Red
            && item1.Yellow == item2.Yellow
            && item1.Blue == item2.Blue
            );
    }

    public static bool operator !=(RYB item1, RYB item2)
    {
        return (
            item1.Red != item2.Red
            || item1.Yellow != item2.Yellow
            || item1.Blue != item2.Blue
            );
    }

    /// <summary>
    /// Gets or sets red value.
    /// </summary>
    public int Red
    {
        get
        {
            return red;
        }
        set
        {
            red = (value > 255) ? 255 : ((value < 0) ? 0 : value);
        }
    }

    /// <summary>
    /// Gets or sets yellow value.
    /// </summary>
    public int Yellow
    {
        get
        {
            return yellow;
        }
        set
        {
            yellow = (value > 255) ? 255 : ((value < 0) ? 0 : value);
        }
    }

    /// <summary>
    /// Gets or sets blue value.
    /// </summary>
    public int Blue
    {
        get
        {
            return blue;
        }
        set
        {
            blue = (value > 255) ? 255 : ((value < 0) ? 0 : value);
        }
    }

    public RYB(int R, int Y, int B)
    {
        this.red = (R > 255) ? 255 : ((R < 0) ? 0 : R);
        this.yellow = (Y > 255) ? 255 : ((Y < 0) ? 0 : Y);
        this.blue = (B > 255) ? 255 : ((B < 0) ? 0 : B);
    }

    public override bool Equals(Object obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;

        return (this == (RYB)obj);
    }

    public override int GetHashCode()
    {
        return Red.GetHashCode() ^ Yellow.GetHashCode() ^ Blue.GetHashCode();
    }
}