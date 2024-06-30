using UnityEngine;
using UnityEngine.UI;

public class GoToMenuButton : MonoBehaviour
{
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(GoToMainMenu);
    }

    void GoToMainMenu()
    {
        GameManager.Instance.GoToMainMenu();
    }
}