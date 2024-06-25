using UnityEngine;

/// <summary>
/// A hitpoint effect apply to the target to increase of decrease incoming damage
/// </summary>
[CreateAssetMenu(fileName = "ReceiveDamageEffect_", menuName = "Scriptable Objects/Effect/ReceiveDamageEffect")]
public class ReceiveDamageEffectSO : HitpointEffectSO
{
    #region Reset
#if UNITY_EDITOR
    private void Reset()
    {
        hitpointType = HitpointEffectType.Damage;
    }
#endif
    #endregion
}