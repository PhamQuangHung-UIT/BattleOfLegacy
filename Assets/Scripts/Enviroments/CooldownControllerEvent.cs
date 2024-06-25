using System;
using UnityEngine;

public class CooldownControllerEvent : MonoBehaviour
{
    public event Action<CooldownEventArgs> OnCooldownStart;

    public void CallOnCooldownStart(float timeInterval)
    {
        OnCooldownStart?.Invoke(new() { timeInterval = timeInterval });
    }

    public event Action<EventArgs> OnCooldownEnd;

    public void CallOnCooldownEnd()
    {
        OnCooldownEnd?.Invoke(new());
    }
}

public class CooldownEventArgs : EventArgs
{
    public float timeInterval;
} 
