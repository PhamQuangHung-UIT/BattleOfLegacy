using UnityEngine;
using UnityEngine.UI;

public class GoToUpgradeButton : MonoBehaviour
{
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(GoToUpgrade);
    }

    void GoToUpgrade()
    {
        GameManager.Instance.GoToUpgrade();
    }
}