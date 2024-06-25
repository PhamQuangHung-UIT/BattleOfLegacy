using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : UnitBaseState
{
    float seekTargetDistance;

    public RunState(Unit unit) : base(unit) 
    {
        seekTargetDistance = unit.unitDetails.attackRange * 1.5f;
        if (seekTargetDistance < 10)
        {
            seekTargetDistance = 10;
        }
    }

    public override void Enter()
    {
        base.Enter();
        animEvent.CallOnStartUnitAnim(UnitState.Run, 1);
    }

    public override void Update()
    {
        base.Update();
        GameObject currentTarget = NearestTarget(out float distance);
        if (distance <= unit.unitDetails.attackRange)
        {
            nextState = (ai.GetState(UnitState.Attack) as AttackState).Start(currentTarget);
            currentStateEvent = StateEvent.Exit;
        } else {
            float hasteValue = unit.GetEffectValue<HasteEffectSO>(ValueType.Percentage) / 100f;
            Vector2 dir = new(unit.unitDetails.movementSpeed * (1 + hasteValue) * (unit.isEnemy ? -Time.deltaTime : Time.deltaTime), 0);
            unit.transform.Translate(dir);
        }
    }

    public override void Exit()
    {
        base.Exit();
        animEvent.CallOnStopUnitAnim(UnitState.Run);
    }

    private GameObject NearestTarget(out float minDistance)
    {
        // Set the default target as opponent statue
        GameObject currentTarget = Level.Instance.GetStatue(!IsEnemy(unit));
        minDistance = float.MaxValue;
        if (Level.Instance.unitList.Count == 0)
        {
            if (unit.isEnemy)
            {
                minDistance = Mathf.Abs(unit.transform.position.x - currentTarget.GetComponent<BoxCollider2D>().bounds.max.x);
            } else minDistance = Mathf.Min(unit.transform.position.x - currentTarget.GetComponent<BoxCollider2D>().bounds.min.x);
        }

        foreach (var target in Level.Instance.unitList)
        {
            if (IsOpponent(target))
            {
                float distance = Mathf.Abs(unit.transform.position.x - target.transform.position.x);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    currentTarget = target.gameObject;
                }
            }
        }
        return currentTarget;
    }
}
