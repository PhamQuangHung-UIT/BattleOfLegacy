using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "StatueAttributeUpgrade", menuName = "Scriptable Objects/Statue/StatueAttributeUpgrade")]
public class StatueAttributeUpgradeSO : ScriptableObject
{
    [System.Serializable]
    public struct AttributeLevel
    {
        public float amount;
        public int upgradeCost;
    }

    public Sprite statueHealthIcon;

    public AttributeLevel[] statueHealthPerLevelDetails;

    public string StatueHealthDescription(int index)
    {
        return $"Increase the statue health to <color=cyan>{statueHealthPerLevelDetails[index].amount}</color>.";
    }

    public Sprite maxManaIcon;

    public AttributeLevel[] maxManaPerLevelDetails;

    public string MaxManaDescription(int index)
    {
        return $"Increase max mana to {maxManaPerLevelDetails[index].amount} mana.";
    }

    public Sprite manaPerSecondIcon;

    public AttributeLevel[] manaPerSecondPerLevelDetails;

    public string ManaPerSecondDescription(int index)
    {
        return $"Increase mana generating speed to {maxManaPerLevelDetails[index].amount} mana/s.";
    }

    public Sprite goldGainRateIcon;

    public AttributeLevel[] goldGainRatePerLevelDetails;

    public string GoldGainRateDescription(int index)
    {
        return $"Increase the gold dropped from the enemy to {maxManaPerLevelDetails[index].amount * 100}%.";
    }
}