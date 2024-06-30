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
        playerData = new();
        StorageUtils.Load(playerData, playerFileName);
    }

    public void SaveData()
    {
        StorageUtils.Save(playerData, playerFileName);
    }

    public void GoToSelectLevel()
    {
        SceneManager.LoadScene("Select Level", LoadSceneMode.Single);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }

    public void GoToLevel()
    {
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }

    public void GoToUpgrade()
    {
        SceneManager.LoadScene("Upgrade", LoadSceneMode.Single);
    }

    public void GoToBuildScene()
    {
        SceneManager.LoadScene("Build", LoadSceneMode.Single);
    }
}