using System;
using UnityEngine;

public class UnitAnimEvent : MonoBehaviour
{
    public Action<AnimArgs> OnStartUnitAnim;

    public void CallOnStartUnitAnim(UnitState typeAnim, float speed)
    {
        OnStartUnitAnim?.Invoke(new() { typeAnim = typeAnim, speed = speed });
    }

    public Action<AnimArgs> OnStopUnitAnim;

    public void CallOnStopUnitAnim(UnitState typeAnim)
    {
        OnStartUnitAnim?.Invoke(new() { typeAnim = typeAnim });
    }
}

public class AnimArgs : EventArgs
{
    public UnitState typeAnim;

    public float speed;
}
