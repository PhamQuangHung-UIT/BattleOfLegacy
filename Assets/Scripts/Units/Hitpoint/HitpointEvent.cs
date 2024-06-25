using System;
using UnityEngine;

[DisallowMultipleComponent]
public class HitpointEvent : MonoBehaviour
{
    public event Action<HitpointArgs> OnHitpointChange;

    public void CallOnHitPointChange(float value)
    {
        OnHitpointChange?.Invoke(new()
        {
            value = value,
        });
    }
}

public class HitpointArgs : EventArgs
{
    public float value;
}