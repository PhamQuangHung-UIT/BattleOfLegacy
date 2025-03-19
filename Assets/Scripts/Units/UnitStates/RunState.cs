using UnityEngine;

public class RunState : UnitBaseState
{

    public RunState(Unit unit) : base(unit)
    {
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
        }
        else
        {
            float hasteValue = unit.GetEffectValue<HasteEffectSO>(ValueType.Percentage) / 100f;
            Vector3 dir = new(unit.unitDetails.movementSpeed * (1 + hasteValue) * (unit.isEnemy ? -Time.deltaTime : Time.deltaTime), 0, 0);
            unit.transform.Translate(dir);
        }
    }

    public override void Exit()
    {
        //animEvent.CallOnStopUnitAnim(UnitState.Run);
        base.Exit();
    }

    private GameObject NearestTarget(out float minDistance)
    {
        // Set the default target as opponent statue
        GameObject currentTarget = Level.Instance.GetStatue(!unit.isEnemy);
        if (unit.isEnemy)
        {
            minDistance = Mathf.Abs(unit.transform.position.x - currentTarget.GetComponent<BoxCollider2D>().bounds.max.x);
        }
        else minDistance = Mathf.Abs(unit.transform.position.x - currentTarget.GetComponent<BoxCollider2D>().bounds.min.x);

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
