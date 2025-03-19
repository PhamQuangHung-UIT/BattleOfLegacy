using UnityEngine;

/// <summary>
/// A hitpoint effect apply to the target to increase of decrease received healing amount
/// </summary>
[CreateAssetMenu(fileName = "HealEffect_", menuName = "Scriptable Objects/Effect/HealEffect")]
public class HealEffectSO : HitpointEffectSO
{
    #region Reset
#if UNITY_EDITOR
    private void Reset()
    {
        hitpointType = HitpointEffectType.Heal;
    }
#endif
    #endregion
}