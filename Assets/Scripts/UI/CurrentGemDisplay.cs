using TMPro;
using UnityEngine;

public class CurrentGemDisplay : MonoBehaviour
{
    TextMeshProUGUI value;

    private void Start()
    {
        value = GetComponent<TextMeshProUGUI>();
        value.text = GameManager.Instance.playerData.currentGem.ToString();
    }
}