using UnityEngine;

public class HurtState : UnitBaseState
{
    float time;
    public HurtState(Unit unit) : base(unit) { }

    public override void Enter()
    {
        time = 0;
        animEvent.CallOnStartUnitAnim(UnitState.Hurt, 1);
    }

    public override void Update()
    {
        time += Time.deltaTime;
        if (time > 0.5f)
        {
            nextState = ai.GetState(UnitState.Run);
            currentStateEvent = StateEvent.Exit;
        }
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
        //animEvent.CallOnStopUnitAnim(UnitState.Hurt);
    }
}