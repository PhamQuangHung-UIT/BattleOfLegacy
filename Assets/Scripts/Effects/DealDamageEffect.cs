using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DealDamageEffect_", menuName = "Scriptable Objects/Effect/DealDamageEffect")]
public class DealDamageEffectSO : EffectSO
{
    #region Tooltip
    [Tooltip("Damage layer that affect by this effect")]
    #endregion
    public int affectToDamageTypeMask;

    public override IEnumerator ApplyEffect(Unit target)
    {
        // Apply the effect
        if (valueType == ValueType.Absolute)
        {
            target.currentBaseDamage += amount;
        } else
        {
            target.currentBaseDamage += (target.baseDamage * amount / 100);
        }
        target.effectList.Add(this);

        // Wait for effect interval
        yield return new WaitForSeconds(timeInterval);

        // Remove the effect
        if (valueType == ValueType.Absolute)
        {
            target.currentBaseDamage -= amount;
        }
        else
        {
            target.currentBaseDamage -= (target.baseDamage * amount / 100);
        }
        target.effectList.Add(this);
    }

    #region Reset
#if UNITY_EDITOR
    private void Reset()
    {
        affectToDamageTypeMask = DamageLayer.Physics & DamageLayer.BaseAttack;
    }
#endif
    #endregion
}