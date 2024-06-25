using UnityEngine;

[CreateAssetMenu(fileName = "Projectile_", menuName = "Scriptable Objects/Projectile/Projectile")]
public class ProjectileSO : ScriptableObject
{
    #region Tooltip
    [Tooltip("Type of the projectile")]
    #endregion
    public ProjectileType type;

    #region Tooltip
    [Tooltip("Projectile max range")]
    #endregion
    #region Range
    [Range(1, GameConsts.maxProjectileRange)]
    #endregion
    public float maxProjectileRange = 50f;

    #region Tooltip
    [Tooltip("Projectile movement speed")]
    #endregion
    #region Range
    [Range(1, 1000f)]
    #endregion
    public float speed = 50f;

    #region Tooltip
    [Tooltip("Splash radius of projectile when hit the enemy. Applied only if the projectile type is single target")]
    #endregion
    #region Range
    [Range(1, 1000f)]
    #endregion
    public float splashRadius = 0;

    #region Tooltip
    [Tooltip("Animator controller of the projectile")]
    #endregion
    public AnimatorOverrideController animatorController;

    #region Tooltip
    [Tooltip("Attached effect when hit an enemy")]
    #endregion
    public EffectSO attachedEffect;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        ValidateUtilities.Assert(1 < maxProjectileRange && maxProjectileRange < GameConsts.maxProjectileRange);
        ValidateUtilities.Assert(1 < speed && speed < 1000);
        ValidateUtilities.AssertNotNull(animatorController, "Projectile's animatorControlller");
    }
#endif
    #endregion
}