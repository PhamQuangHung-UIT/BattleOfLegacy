using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomSlider : MonoBehaviour
{
    [System.Serializable]
    public class CustomSliderEvent : UnityEvent<float>
    {
    }

    RectTransform rectTransform;
    readonly List<RectTransform> sliderElements = new();
    float widthPerElement;
    int elementCount;
    public int currentIndex;

    public CustomSliderEvent onValueChange;

    public void SetValue(float value)
    {
        currentIndex = (int)(value * 10);
        onValueChange?.Invoke(currentIndex / 10f);
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            sliderElements.Add(transform.GetChild(i).gameObject.GetComponent<RectTransform>());
        }
        elementCount = transform.childCount;

        // Wait for the layout engine complete 
        Invoke(nameof(GetElementWidth), 0.2f);
    }

    private void GetElementWidth()
    {
        widthPerElement = sliderElements[0].rect.width;
    }

    private void Start()
    {
        EnableSliderItemAtCurrentIndex();
    }

    void Update()
    {
        if (InputUtility.TryGetInputPosition(out var inputPosition))
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, inputPosition, null, out var localInputPos);

            if (rectTransform.rect.Contains(localInputPos))
            {
                currentIndex = Mathf.Clamp(Mathf.FloorToInt(localInputPos.x / widthPerElement) + Mathf.CeilToInt(elementCount / 2f), 0, 10);
                EnableSliderItemAtCurrentIndex();
                onValueChange?.Invoke(currentIndex / 10f);
            }
        }
    }

    private void EnableSliderItemAtCurrentIndex()
    {
        for (int i = 0; i < elementCount; i++)
        {
            sliderElements[i].GetComponent<CustomSliderItem>().SetActive(i <= currentIndex);
        }
    }
}