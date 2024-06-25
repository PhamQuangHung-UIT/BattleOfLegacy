using UnityEngine;

public class HurtState : UnitBaseState
{
    float time;
    public HurtState(Unit unit) : base(unit) { }

    public override void Enter()
    {
        animEvent.CallOnStartUnitAnim(UnitState.Hurt, 1);   
    }

    public override void Update()
    {
        time += Time.deltaTime;
        if (time > 0.5f)
            nextState = ai.GetState(UnitState.Idle);
        base.Update();
    }

    public override void Exit()
    {
        animEvent.CallOnStopUnitAnim(UnitState.Hurt);
    }
}