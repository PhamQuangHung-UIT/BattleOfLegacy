using TMPro;
using UnityEngine;

public class CurrentGoldDisplay : MonoBehaviour
{
    TextMeshProUGUI value;

    private void Start()
    {
        value = GetComponent<TextMeshProUGUI>();
        value.text = GameManager.Instance.playerData.currentGold.ToString();
    }
}