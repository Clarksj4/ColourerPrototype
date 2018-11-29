using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourMixer : MonoBehaviour
{
    public Color Colour { get { return ResultantColour.color; } }

    public ColourPicker ColourPicker;
    public Image ResultantColour;
    public Color[] Colours;

    private Dictionary<Color, int> colourParts = new Dictionary<Color, int>();

    // Use this for initialization
    void Start ()
    {
        foreach (Color colour in Colours.OrderBy(c => c.r).ThenBy(c => c.g).ThenBy(c => c.b))
            ColourPicker.AddColour(colour);

        ColourPicker.OnColourPicked += HandleOnColourPicked;
    }

    private Color CombineColours(IEnumerable<KeyValuePair<Color, int>> colourParts)
    {
        int count = colourParts == null ? 0 : colourParts.Count();
        if (count == 0)
            return Color.white;

        Color result = Mix(colourParts);

        return result;
    }

    public void Clear()
    {
        ColourPicker.RemoveAllColours();
        ResultantColour.color = CombineColours(null);
    }

    private void HandleOnColourPicked(Color colour)
    {
        ResultantColour.color = CombineColours(ColourPicker.Colours);
    }

    //private ColorCYMK ToCYMK(Color colour)
    //{
    //    float c = 1f - colour.r;
    //    float m = 1f - colour.g;
    //    float y = 1f - colour.b;
    //    float k = Mathf.Min(c, m, y);
    //    float a = colour.a;

    //    c = (c - k) / (1 - k);
    //    m = (m - k) / (1 - k);
    //    y = (y - k) / (1 - k);

    //    return new ColorCYMK
    //    {
    //        Cyan = c,
    //        Magenta = m,
    //        Yellow = y,
    //        Black = k,
    //        Alpha = a
    //    };
    //}

    //private Color ToRGBA(ColorCYMK colour)
    //{
    //    float r = (colour.C + colour.K) * (1f - colour.K);
    //    float g = (colour.M + colour.K) * (1f - colour.K);
    //    float b = (colour.Y + colour.K) * (1f - colour.K);
    //    r = (1f - r);
    //    g = (1f - g);
    //    b = (1f - b);
    //    float a = colour.A;

    //    return new Color(r, g, b, a);
    //}

    //private Color32 Mix(IEnumerable<Color> colours)
    //{
    //    double l = 0;
    //    double a = 0;
    //    double b = 0;

    //    foreach (Color colour in colours)
    //    {
    //        CIELab labColour = ColourSpaceConverter.RGBtoLab((int)(colour.r * 255), (int)(colour.g * 255), (int)(colour.b * 255));
    //        l += labColour.L;
    //        a += labColour.A;
    //        b += labColour.B;
    //    }

    //    l /= colours.Count();
    //    a /= colours.Count();
    //    b /= colours.Count();

    //    RGB asRGB = ColourSpaceConverter.LabtoRGB(l, a, b);

    //    return new Color32((byte)asRGB.Red, (byte)asRGB.Green, (byte)asRGB.Blue, (byte)(colours.Sum(c => c.a * 255) / colours.Count()));
    //}

    private Color Mix(IEnumerable<KeyValuePair<Color, int>> colourParts)
    {
        int r = 0;
        int y = 0;
        int b = 0;
        int nParts = colourParts.Sum(c => c.Value);

        //Color first = colourParts.First().Key;
        //RYB result = ColourSpaceConverter.RGBToRYB((int)(first.r * 255), (int)(first.g * 255), (int)(first.b * 255));
        //r = result.Red;
        //y = result.Yellow;
        //b = result.Blue;

        foreach (KeyValuePair<Color, int> colourMix in colourParts)
        {
            //r = (int)(255 - Math.Sqrt(((255 - r) ^ 2 + (255 - (int)(colour.r * 255.0)) ^ 2) / 2));
            //g = (int)(255 - Math.Sqrt(((255 - g) ^ 2 + (255 - (int)(colour.g * 255.0)) ^ 2) / 2));
            //b = (int)(255 - Math.Sqrt(((255 - b) ^ 2 + (255 - (int)(colour.b * 255.0)) ^ 2) / 2));

            //r = (int)(r * colour.r) / 255;
            //g = (int)(g * colour.g) / 255;
            //b = (int)(b * colour.b) / 255;
            float ratio = colourMix.Value / (float)nParts;
            Color colour = colourMix.Key * ratio;

            RYB ryb = ColourSpaceConverter.RGBToRYB((int)(colour.r * 255), (int)(colour.g * 255), (int)(colour.b * 255));
            r += ryb.Red;
            y += ryb.Yellow;
            b += ryb.Blue;
        }

        RGB rgb = ColourSpaceConverter.RYBToRGB(r, y, b);

        //return new Color32((byte)r, (byte)g, (byte)b, (byte)(colours.Sum(c => c.a * 255) / colours.Count()));
        return new Color(rgb.Red / 255f, rgb.Green / 255f, rgb.Blue / 255f);
    }



    //NewColor.R = 255 - SQRT(((255-Color1.R)^2 + (255-Color2.R)^2)/2)
    //NewColor.G = 255 - SQRT(((255-Color1.G)^2 + (255-Color2.G)^2)/2)
    //NewColor.B = 255 - SQRT(((255-Color1.B)^2 + (255-Color2.B)^2)/2)
}
