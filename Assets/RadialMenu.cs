using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour
{
    public Image Element;
    public float ElementDiameter;
    public float Padding = 5f;
    public int n = 2;

    public Color[] colours;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = transform as RectTransform;
    }

    // Use this for initialization
    void Start () {
        RotateElements();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //private void PositionElements()
    //{
    //    float distance = (rectTransform.sizeDelta.y / 2) - (ElementDiameter / 2) - Padding;

    //    float a = ElementDiameter + Padding;
    //    float b = distance;
    //    float c = distance;

    //    float A = ((a * a) - (b * b) - (c * c)) / (-2 * b * c);
    //    A = Mathf.Cos(A) * (180 / Mathf.PI);
    //    //A = 180 - (2 * A);

    //    for (int i = 0; i < n; i++)
    //    {
    //        Vector2 position = Quaternion.AngleAxis(A * i, Vector3.forward) * (Vector2.up * distance);
    //        RectTransform element = Instantiate(Element, transform);
    //        element.anchoredPosition = position;
    //    }
    //}

    private void RotateElements()
    {
        float angleIncrement = 360f / colours.Length;

        for (int i = 0; i < colours.Length; i++)
        {
            Image element = Instantiate(Element, transform);
            element.transform.rotation = Quaternion.AngleAxis(angleIncrement * i, Vector3.forward);
            element.fillAmount = 1f / colours.Length;
            element.color = colours[i];
        }
    }
}
