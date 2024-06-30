using System;
using System.Collections;
using UnityEngine;

public class AttackState : UnitBaseState
{
    public GameObject target;

    private readonly UnitEvent unitEvent;
    private Statue opponentStatue;
    private Unit targetUnit;
    private Coroutine coroutine;

    public AttackState(Unit unit) : base(unit)
    {
        unitEvent = unit.GetComponent<UnitEvent>();
    }

    public UnitBaseState Start(GameObject target)
    {
        this.target = target;
        opponentStatue = target.GetComponent<Statue>();
        targetUnit = target.GetComponent<Unit>();
        return base.Start();
    }

    public override void Enter()
    {
        base.Enter();
        coroutine = unit.StartCoroutine(StartAttackLoop());
    }

    IEnumerator StartAttackLoop()
    {
        float time = 0;
        while (CanAttackTarget())
        {
            float attackInterval = 1 / unit.currentAttackSpeed;
            if (Time.time - time < attackInterval)
            {
                yield return null;
            }
            else
            {
                //animEvent.CallOnStopUnitAnim(UnitState.Idle);
                animEvent.CallOnStartUnitAnim(UnitState.Attack, 1);
                float attackStrikeDelay = unit.unitDetails.attackAnimationDelay * unit.currentAttackSpeed / unit.unitDetails.strikesPerAttack;
                for (int i = 0; i < unit.unitDetails.strikesPerAttack; i++)
                {
                    SoundManager.Instance.PlaySound(unit.unitDetails.attackSFX);
                    // Wait for animation delay time end
                    yield return new WaitForSeconds(attackStrikeDelay);
                    if (!CanAttackTarget())
                        break;
                    if (unit.unitDetails.isRangeUnit)
                    {
                        var projectile = PoolManager.Instance.ReuseComponent<Projectile>(unit.transform.position, Quaternion.Euler(0, unit.isEnemy ? 180 : 0, 0));
                        projectile.Initialized(unit, unit.currentBaseDamage, unit.unitDetails.attackProjectile);
                        projectile.gameObject.SetActive(true);
                    }
                    else
                    {
                        target.GetComponent<HitpointEvent>().CallOnHitPointChange(-unit.currentBaseDamage);
                        unitEvent.CallOnAttack(target);
                    }
                }
                //animEvent.CallOnStopUnitAnim(UnitState.Attack);
                animEvent.CallOnStartUnitAnim(UnitState.Idle, 1);


                // Reset attack time
                time = Time.time;
            }
        }
        nextState = ai.GetState(UnitState.Run);
        currentStateEvent = StateEvent.Exit;
    }

    private bool CanAttackTarget() => 
        opponentStatue != null && opponentStatue.currentHealth > 0 || targetUnit != null && !targetUnit.isDead;

    public override void Exit()
    {
        if (coroutine != null)
            unit.StopCoroutine(coroutine);
        base.Exit();
    }
}
