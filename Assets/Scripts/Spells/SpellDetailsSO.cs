using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellDetails_", menuName = "Scriptable Objects/Spell/SpellDetails")]
public class SpellDetailsSO : ScriptableObject
{
    [System.Serializable]
    public struct SpellAttribute
    {
        public string name;
        public float value;
    }
    #region Header
    [Header("Basic info")]
    #endregion
    #region Tooltip
    [Tooltip("Name of the spell")]
    #endregion
    public string spellName;

    public string spellDescription = "None";

    public Sprite image;

    #region Header
    [Header("Target")]
    #endregion
    #region Tooltip
    [Tooltip("Target type")]
    #endregion
    public TargetType target;

    #region Tooltip
    [Tooltip("Spell cooldown in seconds")]
    #endregion
    #region Range
    [Range(0.1f, 100f)]
    #endregion
    public float spellCooldown;

    #region Tooltip
    [Tooltip("Mana cost of the spell to be cast")]
    #endregion
    public float manaCost;

    #region Tooltips
    [Tooltip("List of spell upgrades")]
    #endregion
    public SpellAttribute[][] spellUpgradeList;

    #region Tooltip
    [Tooltip("Spell prefab")]
    #endregion
    public GameObject spellPrefab;

    [HideInInspector] public List<Dictionary<string, float>> spellAttributeMapPerLevel;

    private void Awake()
    {
        spellAttributeMapPerLevel = new();
        foreach (var spellAttributes in spellUpgradeList)
        {
            Dictionary<string, float> dict = new();
            foreach (var attribute in spellAttributes)
            {
                dict.Add(attribute.name, attribute.value);
            }
            spellAttributeMapPerLevel.Add(dict);
        }
    }

#if UNITY_EDITOR
    public void OnValidate()
    {
        ValidateUtilities.AssertNotNull(spellName, "spellName " + nameof(SpellDetailsSO));
        ValidateUtilities.AssertNotNull(spellPrefab, "spellPrefab" + nameof(SpellDetailsSO));
        ValidateUtilities.AssertEmptyList(spellUpgradeList);
    }
#endif
}
