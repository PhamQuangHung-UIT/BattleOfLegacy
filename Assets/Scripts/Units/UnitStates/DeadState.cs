using System.Collections;
using UnityEngine;

public class DeadState : UnitBaseState
{
    public DeadState(Unit unit) : base(unit) 
    { }

    public override void Enter()
    {
        base.Enter();
        animEvent.CallOnStartUnitAnim(UnitState.Dead, 1);
        unit.StartCoroutine(DisableUnit());
    }

    private IEnumerator DisableUnit()
    {
        yield return new WaitForSeconds(3);
        unit.gameObject.SetActive(false);
    }

    public override void Exit()
    {
        base.Exit();
        //animEvent.CallOnStopUnitAnim(UnitState.Dead);
    }
}
