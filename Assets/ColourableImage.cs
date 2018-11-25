using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourableImage : MonoBehaviour
{
    public Image PartPrefab;
    public string ImageName;
    public Transform PartsParent;
    public Image Outline;

    private List<Image> partImages = new List<Image>();

	// Use this for initialization
	void Start ()
    {
        // TODO: load outline image from resources
        Outline.sprite = Resources.Load<Sprite>("Sprites/" + ImageName + "/Outline");

        // TODO: load all parts into a new prefab instance
        Sprite[] parts = Resources.LoadAll<Sprite>("Sprites/" + ImageName + "/Parts");
        foreach (Sprite part in parts)
        {
            Image partImage = Instantiate(PartPrefab, PartsParent);
            partImages.Add(partImage);
            partImage.sprite = part;
            partImage.preserveAspect = true;
            partImage.raycastTarget = false;
        }
	}

    public void SetColourAtPosition(Vector2 screenPosition, Color colour)
    {
        Image part = GetPartAtPosition(screenPosition);
        if (part != null)
            part.color = colour;
    }

    public Rect ToScreenSpaceRect(RectTransform rectTransform)
    {
        Vector2 size = Vector2.Scale(rectTransform.rect.size, rectTransform.lossyScale);
        Rect rect = new Rect((Vector2)rectTransform.position - (size * 0.5f), size);
        return rect;
    }

    private Image GetPartAtPosition(Vector2 screenPosition)
    {
        // Screen space rect of outline transform
        Rect screenSpaceRect = ToScreenSpaceRect(Outline.transform as RectTransform);
        if (screenSpaceRect.Contains(screenPosition))
        {
            // Mouse position relative to outline transform
            Vector2 localMousePosition = screenPosition - screenSpaceRect.position;

            // Position as a percent of size
            float normalizedX = localMousePosition.x / screenSpaceRect.width;
            float normalizedY = localMousePosition.y / screenSpaceRect.height;

            // Get pixel coordinate at that position on texture
            int pixelX = (int)(normalizedX * Outline.sprite.texture.width);
            int pixelY = (int)(normalizedY * Outline.sprite.texture.height);

            // Check which part has a coloured pixel in that location
            foreach (Image part in partImages)
            {
                Color pixel = part.sprite.texture.GetPixel(pixelX, pixelY);
                if (pixel.a > 0)
                    return part;
            }
        }

        return null;
    }
}
