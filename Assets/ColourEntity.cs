using System;
using UnityEngine;
using UnityEngine.UI;

public class ColourEntity : MonoBehaviour
{
    public event EventHandler OnTapped; 

    public Color Colour
    {
        get { return image.color; }
        set { image.color = value; }
    }

    public int Count
    {
        get { return count; }
        set
        {
            count = value;
            countText.text = count.ToString();
        }
    }

    private Color colour;
    private Image image;
    private int count;
    private Text countText;

    private void Awake()
    {
        image = GetComponentInChildren<Image>();
        countText = GetComponentInChildren<Text>();
    }

    public void OnTap()
    {
        if (OnTapped != null)
            OnTapped(this, EventArgs.Empty);
    }
}
