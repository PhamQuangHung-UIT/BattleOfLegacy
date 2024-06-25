using System.Collections;
using UnityEngine;

public class EffectSO : ScriptableObject
{
    #region Tooltip
    [Tooltip("Name of the effect")]
    #endregion
    public string effectName;

    #region Tooltip
    [Tooltip("The value type of the effect. Percentage indicates that the value affect relatively base on the unit's base stats. Absolute indicates that the value affect absolutely to the unit's stat")]
    #endregion
    public ValueType valueType;

    public float amount;

    public LayerMask affectTargetLayer;

    #region Tooltip
    [Tooltip("Time interval apply this effect")]
    #endregion
    public float timeInterval = 2f;

    /// <summary>
    /// Override this method to customize how effect apply to the target
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public virtual IEnumerator ApplyEffect(Unit target)
    {
        target.effectList.Add(this);
        yield return new WaitForSeconds(timeInterval);
        target.effectList.Remove(this);
    }
}