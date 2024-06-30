using System;
using UnityEngine;

[DisallowMultipleComponent]
public class LevelManager : SingletonMonoBehaviour<LevelManager>
{
    private const string fileName = "levelManager.dat";

    public int currentLevelIndex;

    public LevelDetailsSO[] levelDetails;
    public LevelManagerSerializableData data;

    public LevelManager() {
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
        data.currentLevel = currentLevelIndex;
        StorageUtils.Save(data, fileName);
    }
}