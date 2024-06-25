using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellBase : MonoBehaviour
{
    [HideInInspector] public SpellDetailsSO spellDetails;
    [HideInInspector] public int level = 1;

    public void Initialized(SpellDetailsSO spellDetails, int level)
    {
        this.spellDetails = spellDetails;
        this.level = level;
    }
    /// <summary>
    /// Execute the spell at the frame time
    /// </summary>
    public abstract void Execute();
}
