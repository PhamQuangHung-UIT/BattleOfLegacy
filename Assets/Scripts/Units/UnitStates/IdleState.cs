public class IdleState : UnitBaseState
{

    public IdleState(Unit unit) : base(unit) 
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        animEvent.CallOnStartUnitAnim(UnitState.Idle, 1);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit() 
    { 
        base.Exit();
        animEvent.CallOnStopUnitAnim(UnitState.Idle);
    }
}
