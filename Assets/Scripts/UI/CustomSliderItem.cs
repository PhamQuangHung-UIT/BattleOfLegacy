using UnityEngine;
using UnityEngine.UI;

public class CustomSliderItem : MonoBehaviour
{
    public Sprite turnOnImage;
    public Sprite turnOffImage;

    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetActive(bool active)
    {
        image.sprite = active ? turnOnImage : turnOffImage;
    }
}