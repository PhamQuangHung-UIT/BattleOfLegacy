using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatueAttributeUpgrade", menuName = "Scriptable Objects/Statue/StatueAttributeUpgrade")]
public class StatueAttributeUpgradeSO : ScriptableObject
{
    [System.Serializable]
    public struct StatueAttributeUpgrade
    {
        public float amount;
        public bool isGemUpgrade;
        public int upgradeCost;
    }

    public struct StatueAttribute
    {
        public string name;
        public string title;
        public string upgradeTitle;
        public string description;
        public Sprite icon;
        public StatueAttributeUpgrade[] upgradeDetails;
        public ValueType valueType;

        public StatueAttribute(string name, string title, string upgradeTitle, string description, Sprite icon, StatueAttributeUpgrade[] upgradeDetails, ValueType valueType)
        {
            this.name = name;
            this.title = title;
            this.upgradeTitle = upgradeTitle;
            this.description = description;
            this.icon = icon;
            this.upgradeDetails = upgradeDetails;
            this.valueType = valueType;
        }
    }
    // Statue Health
    public string statueHealthTitle = "Max health";

    public string statueHealthUpgradeTitle = "Max Health Increase";

    public Sprite statueHealthIcon;

    public StatueAttributeUpgrade[] statueHealthPerLevelDetails;

    public string statueHealthDescription = "Increase statue max health";

    // Max mana
    public string maxManaTitle = "Max mana";

    public string maxManaUpgradeTitle = "Max Mana Increase";

    public Sprite maxManaIcon;

    public StatueAttributeUpgrade[] maxManaPerLevelDetails;

    public string maxManaDescription = "Increase maximum mana can be held";

    // Mana Recover Speed
    public string manaRecoverSpeedTitle = "Mana recover speed";

    public string manaRecoverSpeedUpgradeTitle = "Mana Recover Speed Increase";

    public Sprite manaRecoverSpeedIcon;

    public StatueAttributeUpgrade[] manaRecoverSpeedPerLevelDetails;

    public string manaRecoverSpeedDescription = "Increase mana recovering speed";

    // Gold gain rate
    public string goldGainRateTitle = "Gold gain rate";

    public string goldGainRateUpgradeTitle = "Gold Rate Increase";

    public Sprite goldGainRateIcon;

    public StatueAttributeUpgrade[] goldGainRatePerLevelDetails;

    public string goldGainRateDescription = "Increase the gold receving from the enemy";

    // Spell slot
    public string maxSpellSlotTitle = "Maximum carried spell";

    public string maxSpellSlotUpgradeTitle = "Maximum Spell Slot Increase";

    public Sprite maxSpellSlotIcon;

    public StatueAttributeUpgrade[] maxSpellSlotPerLevelDetails;

    public string maxSpellSlotDescription = "Increase the maximum spell slot a player can hold";

    public Dictionary<string, StatueAttribute> attributeList = new();

    public void Init()
    {
        attributeList.Add("statueHealth", new("statueHealth",
                                              statueHealthTitle,
                                              statueHealthUpgradeTitle,
                                              statueHealthDescription,
                                              statueHealthIcon,
                                              statueHealthPerLevelDetails,
                                              ValueType.Absolute));
        attributeList.Add("maxMana", new("maxMana",
                                         maxManaTitle,
                                         maxManaUpgradeTitle,
                                         maxManaDescription,
                                         maxManaIcon,
                                         maxManaPerLevelDetails,
                                         ValueType.Absolute));
        attributeList.Add("manaRecoverSpeed", new("manaRecoverSpeed",
                                                  manaRecoverSpeedTitle,
                                                  manaRecoverSpeedUpgradeTitle,
                                                  manaRecoverSpeedDescription,
                                                  manaRecoverSpeedIcon,
                                                  manaRecoverSpeedPerLevelDetails,
                                                  ValueType.Absolute));
        attributeList.Add("goldGainRate", new("goldGainRate",
                                              goldGainRateTitle,
                                              goldGainRateUpgradeTitle,
                                              goldGainRateDescription,
                                              goldGainRateIcon,
                                              goldGainRatePerLevelDetails,
                                              ValueType.Percentage));
        attributeList.Add("maxSpellSlot", new("maxSpellSlot",
                                               maxSpellSlotTitle,
                                               maxSpellSlotUpgradeTitle,
                                               maxSpellSlotDescription,
                                               maxSpellSlotIcon,
                                               maxSpellSlotPerLevelDetails,
                                               ValueType.Absolute));
    }
}