using System;
using UnityEngine;

/// <summary>
/// Base unit's component to manage all events related to this unit
/// </summary>
[DisallowMultipleComponent]
public class UnitEvent : MonoBehaviour
{
    public event Action<EventArgs> OnHurt;

    public void CallOnHurt()
    {
        OnHurt?.Invoke(new());
    }

    public event Action<HitpointArgs> OnShieldAdded;

    public void CallOnShieldAdded(float value)
    {
        OnShieldAdded?.Invoke(new() { value = value });
    }

    public event Action<HitpointArgs> OnShieldRemoved;

    public void CallOnShieldRemoved(float value)
    {
        OnShieldRemoved?.Invoke(new() { value = value });
    }

    public event Action<EventArgs> OnDeath;

    public void CallOnDead() => OnDeath?.Invoke(new());

    public event Action<AttackArgs> OnAttack;

    public void CallOnAttack(GameObject target)
    {
        OnAttack?.Invoke(new() { target = target });
    }
}

public class AttackArgs
{
    public GameObject target;
}