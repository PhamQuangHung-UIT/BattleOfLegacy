using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameManagerSettings_",menuName = "Scriptable Objects/GameManager/GameManagerSettings")]
public class GameManagerSettingsSO: ScriptableObject
{
    [System.Serializable]
    public struct LevelMusicPair
    {
        public Theme levelType;
        public AudioClip music;
    }

    public UnitBaseStatsSO[] allObtainableUnit;

    public SpellBase[] allObtainableSpell;

    #region Tooltip
    [Tooltip("Tint color to be used when the unit is disabled")]
    #endregion
    public Color unitDisableColor = Color.grey;

    #region Header
    [Header("Background Music")]
    #endregion
    public AudioClip introMusic;

    public LevelMusicPair[] levelMusic;

    public Dictionary<Theme, AudioClip> levelMusicDict;

    private void Awake()
    {
        levelMusicDict = new();
        foreach (var item in levelMusic)
        {
            levelMusicDict.Add(item.levelType, item.music);
        }
    }

}