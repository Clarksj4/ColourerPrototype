using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageColourer : MonoBehaviour
{
    public ColourableImage ColourableImage;
    public ColourMixer ColourMixer;

    public void OnTap()
    {
        ColourableImage.SetColourAtPosition(Input.mousePosition, ColourMixer.Colour);
    }
}
