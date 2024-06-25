using System.Collections;
using UnityEngine;

public class AttackState : UnitBaseState
{
    public GameObject target;

    private UnitEvent unitEvent;

    public AttackState(Unit unit) : base(unit)
    {
        unitEvent = unit.GetComponent<UnitEvent>();
    }

    public UnitBaseState Start(GameObject target)
    {
        this.target = target;
        return base.Start();
    }

    public override void Enter()
    {
        base.Enter();
        unit.StartCoroutine(StartAttackLoop());
    }

    IEnumerator StartAttackLoop()
    {
        float time = Time.time;
        while (target.activeSelf)
        {
            float attackInterval = 1 / unit.currentAttackSpeed;
            if (Time.time - time < attackInterval)
            {
                yield return null;
                continue;
            }
            else
            {
                animEvent.CallOnStartUnitAnim(UnitState.Attack, unit.currentAttackSpeed);
                float attackStrikeDelay = unit.unitDetails.attackAnimationDelay * unit.currentAttackSpeed / unit.unitDetails.strikesPerAttack;
                for (int i = 0; i < unit.unitDetails.strikesPerAttack; i++)
                {
                    // Wait for animation delay time end
                    yield return new WaitForSeconds(attackStrikeDelay);
                    if (!target.activeSelf)
                        break;
                    if (unit.unitDetails.isRangeUnit)
                    {
                        var projectile = PoolManager.Instance.ReuseComponent<Projectile>(unit.transform.position, Quaternion.Euler(0, unit.isEnemy ? 180 : 0, 0));
                        projectile.Initialized(new(unit.isEnemy ? -1 : 1, 0), unit.currentBaseDamage, unit.unitDetails.attackProjectile);
                        projectile.gameObject.SetActive(true);
                    }
                    else
                    {
                        target.GetComponent<HitpointEvent>().CallOnHitPointChange(-unit.currentBaseDamage);
                        unitEvent.CallOnAttack(target);
                    }
                }

                // Reset attack time
                time = Time.time;
            }
        }
    }

    public override void Exit()
    {
        unit.StopCoroutine(StartAttackLoop());
        base.Exit();
    }
}
