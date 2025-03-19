using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameManagerSettings_", menuName = "Scriptable Objects/GameManager/GameManagerSettings")]
public class GameManagerSettingsSO : ScriptableObject
{
    [System.Serializable]
    public struct LevelMusicPair
    {
        public Theme levelType;
        public AudioClip music;
    }

    public UnitBaseStatsSO[] allObtainableUnit;

    public SpellBase[] allObtainableSpell;

    public UnitBaseStatsSO[] beginUnitAlignment;

    public SpellBase[] beginSpellAlignment;

    #region Header
    [Header("Background Music")]
    #endregion
    public AudioClip introMusic;

    public LevelMusicPair[] levelMusic;

    public Dictionary<Theme, AudioClip> levelMusicDict;

    #region Header
    [Header("Color Settings")]
    #endregion
    public Color defaultAttributeColor = new(0.9f, 0.9f, 0.9f);

    public Color goldValueColor = Color.white;

    public Color gemValueColor = Color.red;

    public Color maxHealthAttributeColor = Color.red;

    public Color attackDamageAttributeColor = Color.yellow;

    public Color statueAttributeColor = Color.cyan;

    public Color disableColor = new(0.3f, 0.3f, 0.3f);

    public Color disableColorForCover = new(0.3f, 0.3f, 0.3f, 0.7f);

    #region Header
    [Header("Icons")]
    #endregion
    public Sprite healthIcon;

    public Sprite attackIcon;

    public Sprite attackRangeIcon;

    public Sprite speedIcon;

    public Sprite timeIcon;

    public Sprite gemIcon;

    public Sprite goldIcon;

    private void Awake()
    {
        levelMusicDict = new();
        foreach (var item in levelMusic)
        {
            levelMusicDict.Add(item.levelType, item.music);
        }
    }

}