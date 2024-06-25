using UnityEngine;

[SerializeField]
public struct UnitUpgradeDetails
{
    public int cost;
    public int unitMaxHealth;
    public int unitDamage;
    public bool isGoldUpgrade;
    public bool isGemUpgrade;
    public bool unlockFirstSkill;
    public bool unlockSecondSkill;
}