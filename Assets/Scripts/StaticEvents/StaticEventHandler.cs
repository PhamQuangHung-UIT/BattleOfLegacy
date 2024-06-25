using System;

/// <summary>
/// Base class for all static event handlers happen in the game
/// </summary>
public static class StaticEventHandler
{
    public static event Action<CastSpellArgs> OnCastSpell;

    public static void CallOnCastSpell(SpellBase spell)
    {
        OnCastSpell?.Invoke(new() { spell = spell });
    }

    public static event Action<EventArgs> OnVictory;

    public static void CallOnVictory() => OnVictory?.Invoke(new());

    public static event Action<EventArgs> OnDefeat;

    public static void CallOnDefeat() => OnDefeat?.Invoke(new());

    public static event Action<EventArgs> OnPause;

    public static void CallOnPause() => OnPause?.Invoke(new());

    public static event Action<OnReceiveGoldEventArgs> OnReceiveGold;

    public static void CallOnReceiveGold(int amount)
    {
        OnReceiveGold?.Invoke(new() { amount = amount });
    }
}

public class CastSpellArgs: EventArgs
{
    public SpellBase spell;
} 

public class OnReceiveGoldEventArgs : EventArgs
{
    public int amount;
}