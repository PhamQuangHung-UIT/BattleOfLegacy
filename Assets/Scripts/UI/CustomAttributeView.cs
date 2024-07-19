using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomAttributeView : MonoBehaviour
{
    public TextMeshProUGUI attributeTitle, attributeValue;
    public Image attributeIcon;
    
    public void SetUp(string title, object value, ValueType valueType, Sprite icon)
    {
        attributeTitle.text = title;
        attributeValue.text = valueType == ValueType.Percentage ? $"{value:P}" : $"{value}";
        attributeIcon.sprite = icon;
    }

    public void SetColor(Color color)
    {
        attributeTitle.color = color;
        attributeValue.color = color;
    }
}
