using UnityEngine;
using UnityEngine.UI;

public class GoToBuildSceneButton : MonoBehaviour
{
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(GoToBuildScene);
    }

    private void GoToBuildScene()
    {
        GameManager.Instance.GoToBuildScene();
    }
}