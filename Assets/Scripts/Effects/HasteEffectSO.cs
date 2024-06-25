using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "HasteEffect_", menuName = "Scriptable Objects/Effect/HasteEffect")]
public class HasteEffectSO : EffectSO
{
    #region Tooltip
    [Tooltip("Available time for the haste effect")]
    #endregion
    public float availableTime;

    public override IEnumerator ApplyEffect(Unit target)
    {
        target.effectList.Add(this);
        target.currentAttackSpeed += target.unitDetails.attackSpeed * amount / 100;
        yield return new WaitForSeconds(availableTime);
        target.currentAttackSpeed += target.unitDetails.attackSpeed * amount / 100;
        target.effectList.Remove(this);
    }

    #region Unity Editor
#if UNITY_EDITOR
    private void Reset()
    {
        amount = 30;
        availableTime = 1;
        valueType = ValueType.Percentage;
    }
#endif
    #endregion
}