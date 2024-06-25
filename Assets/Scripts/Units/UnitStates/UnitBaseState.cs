using UnityEngine;

public class UnitBaseState
{
    public enum StateEvent { Enter, Update, Exit }

    public UnitBaseState nextState;

    public StateEvent currentStateEvent = StateEvent.Enter;

    public Unit unit;

    public UnitAnimEvent animEvent;

    public UnitAI ai;

    private readonly int enemyLayer = LayerMask.NameToLayer("Enemy");


    public UnitBaseState(Unit unit)
    {
        this.unit = unit;
        animEvent = unit.GetComponent<UnitAnimEvent>();
        ai = unit.GetComponent<UnitAI>();
    }

    public virtual UnitBaseState Start()
    {
        currentStateEvent = StateEvent.Enter;
        return this;
    }

    /// <summary>
    /// Initialize the unit state. Runs before the Update method.
    /// </summary>
    public virtual void Enter() => currentStateEvent = StateEvent.Update;

    /// <summary>
    /// Run onces per frame. This method handles the AI logic.
    /// </summary>
    public virtual void Update()
    {
        currentStateEvent = StateEvent.Update;
        if (Level.Instance.isGameEnded)
        {
            if (this is IdleState)
                return;
            nextState = ai.GetState(UnitState.Idle).Start();
            currentStateEvent = StateEvent.Exit;
        }
    }

    /// <summary>
    /// Exit the unit state to get to next state. Use this method to stop state animation
    /// </summary>
    public virtual void Exit() => currentStateEvent = StateEvent.Exit;

    /// <summary>
    /// Base method use in the state machine
    /// </summary>
    /// <returns></returns>
    public UnitBaseState Process()
    {
        switch (currentStateEvent)
        {
            case StateEvent.Enter:
                Enter();
                return this;
            case StateEvent.Update:
                Update();
                return this;
            default:
                Exit();
                return nextState;
        }
    }

    protected bool IsOpponent(Unit u)
    {
        return ((u.gameObject.layer ^ unit.gameObject.layer) & enemyLayer) != 0;
    }

    protected bool IsEnemy(Unit u)
    {
        return (u.gameObject.layer & enemyLayer) != 0;
    }
}
