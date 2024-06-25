using UnityEngine;

public abstract class SpecialSkillState : UnitBaseState
{
    /// <summary>
    /// Specify whenether the skill is unlocked
    /// </summary>
    public bool isUnlocked;

    public string description;

    public SpecialSkillState(Unit unit) : base(unit) { }

    /// <summary>
    /// Called every frame to check whenether ApplySkill() can be called
    /// </summary>
    /// <returns>true if special skill can be applied, otherwise false</returns>
    public abstract bool PreCondition();

    /// <summary>
    /// Implement this method for special skill. Can be called if PreCondition() return true
    /// </summary>
    public abstract void ApplySkill();
} 