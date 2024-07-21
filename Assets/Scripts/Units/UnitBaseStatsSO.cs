using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitDetails_", menuName = "Scriptable Objects/Unit/UnitBaseStats")]
public class UnitBaseStatsSO : ScriptableObject
{
    #region Header
    [Header("General")]
    #endregion
    #region Tooltip
    [Tooltip("Avatar image that will be shown at the Unit slot ")]
    #endregion
    public Sprite image;

    #region Tooltip
    [Tooltip("Name of the unit")]
    #endregion
    public string unitName;

    #region Tooltip
    [Tooltip("Description of the unit")]
    #endregion
    public string unitDescription;

    #region Tooltip
    [Tooltip("Specify whether it is the range unit")]
    #endregion
    public bool isRangeUnit;

    #region Header
    [Header("Range Attrbute")]
    #endregion
    #region Tooltip
    [Tooltip("Projectile of base attack that is used by range unit only")]
    #endregion
    public ProjectileSO attackProjectile;

    #region Tooltip
    [Tooltip("Come out Y position of the projectile that relative to unit's height")]
    #endregion
    public float castProjectileRelativePosition = 0.5f;

    #region Header
    [Header("Basic Stats")]
    #endregion
    #region Tooltip
    [Tooltip("Mana cost of the unit to be cast")]
    #endregion
    public float manaCost;

    #region Tooltip
    [Tooltip("Time to reload the unit")]
    #endregion
    public float cooldownInterval;

    #region Tooltip
    [Tooltip("Attack speed of the unit")]
    #endregion
    #region Range
    [Range(0.1f, 10f)]
    #endregion
    public float attackSpeed = 1.0f;

    #region Tooltip
    [Tooltip("Attack sound effect")]
    #endregion
    public SoundEffectSO attackSFX;

    #region Tooltip
    [Tooltip("The number of strike to the target for each attack")]
    #endregion
    #region Range
    [Range(1, 5)]
    #endregion
    public int strikesPerAttack = 1;

    #region Tooltip
    [Tooltip("Delay time for animating the base attack")]
    #endregion
    #region Range
    [Range(0.1f, 10f)]
    #endregion
    public float attackAnimationDelay = 1.0f;

    #region Tooltip
    [Tooltip("Critical strike chances")]
    #endregion
    #region Range
    [Range(0f, 1f)]
    #endregion
    public float critStrikeChance = 2;

    #region Tooltip
    [Tooltip("Critical strike damage")]
    #endregion
    #region Range
    [Range(1f, 50f)]
    #endregion
    public float critStrikeDamage;

    #region Tooltip
    [Tooltip("Attack range")]
    #endregion
    #region Range
    [Range(0.1f, 100f)]
    #endregion
    public float attackRange;

    #region Header
    [Header("Movement")]
    #endregion
    #region Tooltip
    [Tooltip("Movement speed of unit")]
    #endregion
    public float movementSpeed;

    #region Header
    [Header("Abilities")]
    #endregion
    #region Tooltip
    [Tooltip("First skill of the unit")]
    #endregion
    public SpecialSkillState firstSkill;

    #region Tooltip
    [Tooltip("Second skill of the unit")]
    #endregion
    public SpecialSkillState secondSkill;

    #region Header
    [Header("Controller")]
    #endregion
    #region Tooltip
    [Tooltip("Animation controller of the unit")]
    #endregion
    public AnimatorOverrideController animationController;

    #region Header
    [Header("Upgrades")]
    #endregion
    #region Tooltip
    [Tooltip("List of unit stats")]
    #endregion
    public UnitUpgradeDetails[] upgradeDetails;

    #region UNITY EDITOR
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (attackProjectile != null)
            isRangeUnit = true;
        if (upgradeDetails != null)
        {
            foreach (var upgrade in upgradeDetails)
            {
                ValidateUtilities.Assert(upgrade.isGoldUpgrade ^ upgrade.isGemUpgrade);
            }
        }
    }
#endif
    #endregion
}
