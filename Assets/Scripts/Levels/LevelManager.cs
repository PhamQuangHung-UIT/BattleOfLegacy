using UnityEngine;

[DisallowMultipleComponent]
public class LevelManager : SingletonMonoBehaviour<LevelManager>
{
    private const string fileName = "levelManager.dat";

    public int currentLevelIndex;

    public LevelDetailsSO[] levelDetails;
    public LevelManagerSerializableData data;

    public LevelManager()
    {
        data = new LevelManagerSerializableData();
    }

    private void Start()
    {
        StorageUtils.Load(data, fileName);
        DontDestroyOnLoad(this);
    }

    public void Save(int selectedLevel)
    {
        data.selectedLevel = selectedLevel;
        StorageUtils.Save(data, fileName);
    }

    public void CompleteLevel()
    {
        if (data.currentLevel == data.selectedLevel && data.currentLevel < levelDetails.Length - 1)
            data.currentLevel++;
        if (data.selectedLevel < levelDetails.Length - 1)
            data.selectedLevel++;
        StorageUtils.Save(data, fileName);
    }
}