using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitAI : MonoBehaviour
{
    UnitBaseState currentState;
    UnitEvent unitEvent;
    Unit unit;

    Dictionary<UnitState, UnitBaseState> unitStates;

    private void Awake()
    {
        unitEvent = GetComponent<UnitEvent>();
        unit = GetComponent<Unit>();
        unitStates = new()
        {
            {UnitState.Idle, new IdleState(unit) },
            {UnitState.Attack, new AttackState(unit) },
            {UnitState.Run, new RunState(unit) },
            {UnitState.Hurt, new HurtState(unit) },
            {UnitState.Dead, new DeadState(unit) },
        };
    }

    private void OnEnable()
    {
        // Reset unit state
        currentState = unitStates[UnitState.Run];
        unitEvent.OnHurt += UnitEvent_OnHurt;
        unitEvent.OnDeath += UnitEvent_OnDeath;
    }

    private void OnDisable() 
    {
        unitEvent.OnHurt -= UnitEvent_OnHurt;
        unitEvent.OnDeath -= UnitEvent_OnDeath;
    }

    private void Update()
    {
        if (unit.unitDetails.firstSkill != null && unit.unitDetails.firstSkill.PreCondition())
        {
            unit.unitDetails.firstSkill.ApplySkill();
        }
        else if (unit.unitDetails.secondSkill != null && unit.unitDetails.secondSkill.PreCondition())
        {
            unit.unitDetails.firstSkill.ApplySkill();
        }
        else
        {
            currentState = currentState.Process();
        }
    }

    public UnitBaseState GetState(UnitState state)
    {
        return unitStates[state];
    }

    private void UnitEvent_OnHurt(EventArgs args)
    {
        currentState.currentStateEvent = UnitBaseState.StateEvent.Exit;
        currentState.nextState = unitStates[UnitState.Hurt];
    }

    private void UnitEvent_OnDeath(EventArgs args)
    {
        currentState.currentStateEvent = UnitBaseState.StateEvent.Exit;
        currentState.nextState = unitStates[UnitState.Dead];
    }
}