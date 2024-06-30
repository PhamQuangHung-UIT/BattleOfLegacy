using UnityEngine;
using UnityEngine.UI;

public class GoToSelectLevel : MonoBehaviour
{
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(GoToSelectLevelScene);
    }

    private void GoToSelectLevelScene()
    {
        GameManager.Instance.GoToSelectLevel();
    }
}