using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ColourPicker : MonoBehaviour
{
    public event Action<Color> OnColourPicked;

    public IEnumerable<KeyValuePair<Color, int>> Colours
    {
        get
        {
            return colours.Values
                          .Where(c => c.Count > 0)
                          .Select(c => new KeyValuePair<Color, int>(c.Colour, c.Count));
        }
    }

    public ColourEntity ColourEntityPrefab;
    public Transform Content;

    private Dictionary<Color, ColourEntity> colours = new Dictionary<Color, ColourEntity>();

    public void AddColour(Color colour, int quantity = 1)
    {
        // If not already in collection add it
        if (!colours.ContainsKey(colour))
        {
            ColourEntity ent = Instantiate(ColourEntityPrefab, Content);
            ent.Colour = colour;
            ent.OnTapped += HandleOnTapped;
            colours.Add(colour, ent);
        }
    }

    public void RemoveColour(Color colour, int quantity = -1)
    {
        if (!colours.ContainsKey(colour))
            return;

        ColourEntity ent = colours[colour];
         ent.Count -= quantity;
    }

    public void RemoveAll(Color colour)
    {
        if (!colours.ContainsKey(colour))
            return;

        ColourEntity ent = colours[colour];
        ent.Count -= ent.Count;
    }

    public void RemoveAllColours()
    {
        foreach (ColourEntity ent in colours.Values)
            ent.Count -= ent.Count;
    }

    private void HandleOnTapped(object sender, EventArgs e)
    {
        ColourEntity ent = sender as ColourEntity;
        Color colour = ent.Colour;
        ent.Count++;

        if (OnColourPicked != null)
            OnColourPicked(colour);
    }
}
