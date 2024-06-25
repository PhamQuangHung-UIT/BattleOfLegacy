using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [HideInInspector] public string playerFileName = "player.dat";
    [HideInInspector] public PlayerGameDataSerializable playerData;

    public GameManagerSettingsSO settings;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        StorageUtils.Load(playerData, playerFileName);
    }

    public void GoToSelectLevel()
    {
        SceneManager.LoadScene("Select Level", LoadSceneMode.Additive);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }

    public void GoToLevel()
    {
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }

    public void GoToShop()
    {
        SceneManager.LoadScene("Upgrade", LoadSceneMode.Additive);
    }
}