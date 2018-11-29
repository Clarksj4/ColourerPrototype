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

            // Clamp it!
            if (count < 0)
                count = 0;

            // Set text
            countText.text = count.ToString();

            // Hide if 0
            bool showCount = count > 0;
            countText.gameObject.SetActive(showCount);
        }
    }

    private Color colour;
    private Image image;
    private int count;
    private Text countText;

    private void Awake()
    {
        image = transform.Find("Mask").Find("Colour").GetComponent<Image>();
        countText = GetComponentInChildren<Text>();
        countText.gameObject.SetActive(false);
    }

    public void OnTap()
    {
        if (OnTapped != null)
            OnTapped(this, EventArgs.Empty);
    }
}
