using System.Collections.Generic;
using System.Linq;
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
        public string description;
        public Sprite icon;
        public StatueAttributeUpgrade[] upgradeDetails;

        public StatueAttribute(string name, string title, string description, Sprite icon, StatueAttributeUpgrade[] upgradeDetails)
        {
            this.name = name;
            this.title = title;
            this.description = description;
            this.icon = icon;
            this.upgradeDetails = upgradeDetails;
        }
    }
    // Statue Health
    public string statueHealthTitle = "Gold Rate Increase";

    public Sprite statueHealthIcon;

    public StatueAttributeUpgrade[] statueHealthPerLevelDetails;

    public string statueHealthDescription  = "Increase statue max health";

    // Max mana
    public string maxManaTitle = "Max Mana Increase";

    public Sprite maxManaIcon;

    public StatueAttributeUpgrade[] maxManaPerLevelDetails;

    public string maxManaDescription = "Increase maximum mana can be held";

    // Mana Recover Speed
    public string manaRecoverSpeedTitle = "Mana Recover Speed Increase";

    public Sprite manaRecoverSpeedIcon;

    public StatueAttributeUpgrade[] manaRecoverSpeedPerLevelDetails;

    public string manaRecoverSpeedDescription = "Increase mana recovering speed";

    // Gold gain rate
    public string goldGainRateTitle = "Gold Rate Increase";

    public Sprite goldGainRateIcon;

    public StatueAttributeUpgrade[] goldGainRatePerLevelDetails;

    public string goldGainRateDescription = "Increase the gold receving from the enemy";

    // Spell slot
    public string maxSpellSlotTitle = "Maximum Spell Slot Increase";

    public Sprite maxSpellSlotIcon;

    public StatueAttributeUpgrade[] maxSpellSlotPerLevelDetails;

    public string maxSpellSlotDescription = "Increase the maximum spell slot a player can hold";

    public Dictionary<string, StatueAttribute> attributeList = new();

    public void Init()
    {
        attributeList.Add("statueHealth", new("statueHealth",
                                              statueHealthTitle,
                                              statueHealthDescription,
                                              statueHealthIcon,
                                              statueHealthPerLevelDetails));
        attributeList.Add("maxMana", new("maxMana",
                                         maxManaTitle,
                                         maxManaDescription,
                                         maxManaIcon,
                                         maxManaPerLevelDetails));
        attributeList.Add("manaRecoverSpeed", new("manaRecoverSpeed",
                                                  manaRecoverSpeedTitle,
                                                  manaRecoverSpeedDescription,
                                                  manaRecoverSpeedIcon,
                                                  manaRecoverSpeedPerLevelDetails));
        attributeList.Add("goldGainRate", new("goldGainRate",
                                              goldGainRateTitle,
                                              goldGainRateDescription,
                                              goldGainRateIcon,
                                              goldGainRatePerLevelDetails));
        attributeList.Add("maxSpellSlot", new("maxSpellSlot",
                                                   maxSpellSlotTitle,
                                                   maxSpellSlotDescription,
                                                   maxSpellSlotIcon,
                                                   maxSpellSlotPerLevelDetails));
    }
}