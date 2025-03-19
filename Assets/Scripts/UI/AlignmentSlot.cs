using UnityEngine;
using UnityEngine.UI;

public class AlignmentSlot : MonoBehaviour
{
    public Image icon;
    public Button button;
    private void Awake()
    {
        button.onClick.AddListener(RemoveItem);
    }

    public void SetItem(Sprite newIcon)
    {
        icon.gameObject.SetActive(true);
        icon.sprite = newIcon;
        icon.color = Color.white;
    }

    public void RemoveItem()
    {
        icon.gameObject.SetActive(false);
        button.onClick.RemoveAllListeners();
    }
}
