using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellBase : MonoBehaviour
{
    public SpellDetailsSO spellDetails;
    [HideInInspector] public int level = 1;
    private void Awake()
    {
        spellDetails.Init();
    }

    public void Initialized(int level)
    {
        this.level = level;
    }
    /// <summary>
    /// Execute the spell at the frame time
    /// </summary>
    public abstract void Execute();
}
