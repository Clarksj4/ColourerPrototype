using UnityEngine;
using System.Collections;

public struct ColorCYMK
{
    public float C
    {
        get { return Cyan; }
        set { Cyan = value; }
    }

    public float Y
    {
        get { return Yellow; }
        set { Yellow = value; }
    }

    public float M
    {
        get { return Magenta; }
        set { Magenta = value; }
    }

    public float K
    {
        get { return Black; }
        set { Black = value; }
    }

    public float A
    {
        get { return Alpha; }
        set { Alpha = value; }
    }

    public float Cyan;
    public float Yellow;
    public float Magenta;
    public float Black;
    public float Alpha;

    public static ColorCYMK operator +(ColorCYMK colour1, ColorCYMK colour2)
    {
        return new ColorCYMK
        {
            C = (float)colour1.C + colour2.C,
            Y = (float)colour1.Y + colour2.Y,
            M = (float)colour1.M + colour2.M,
            K = (float)colour1.K + colour2.K,
            A = (float)colour1.A + colour2.A
        };
    }

    public static ColorCYMK operator /(ColorCYMK colour1, float divisor)
    {
        return new ColorCYMK
        {
            C = colour1.C / divisor,
            Y = colour1.Y / divisor,
            M = colour1.M / divisor,
            K = colour1.K / divisor,
            A = colour1.A / divisor
        };
    }
}
