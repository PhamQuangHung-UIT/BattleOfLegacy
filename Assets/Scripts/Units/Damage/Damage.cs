/*using System.Linq;
using UnityEngine;

[RequireComponent(typeof(UnitEvent))]
[RequireComponent(typeof(Unit))]
[DisallowMultipleComponent]
public class Damage : MonoBehaviour
{
    UnitEvent damageEvent;
    Unit unit;

    void Awake()
    {
        damageEvent = GetComponent<UnitEvent>();
        unit = GetComponent<Unit>();
    }

    private void OnEnable()
    {
        damageEvent.OnDealDamage += DamageEvent_OnDealDamage;
    }

    private void OnDisable()
    {
        damageEvent.OnDealDamage -= DamageEvent_OnDealDamage;
    }


    private void DamageEvent_OnDealDamage(DamageArgs args)
    {
        ApplyDamageEffect(args.attack);
    }

    private void ApplyDamageEffect(Attack attack)
    {
        *//*foreach (DamageEffectSO damageEffect in unit.effectList.Where(e => e is DamageEffectSO && e.valueType == ValueType.Absolute))
        {
            if ((damageEffect.affectToDamageTypeMask & attack.damageLayer) != 0) 
            {
                attack.damage += damageEffect.amount;
            }
        }*//*
        float percentageValue = 0;
        foreach (DealDamageEffectSO damageEffect in unit.effectList.Where(e => e is DealDamageEffectSO && e.valueType == ValueType.Percentage))
        {
            if ((damageEffect.affectToDamageTypeMask & attack.damageLayer) != 0)
            {
                percentageValue += damageEffect.amount;
            }
        }
        attack.damage += attack.damage * percentageValue;
    }

    *//*private IEnumerator AttackEnemy(Attack attack, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        foreach (var target in attack.targets)
        {
            var unitEvent = target.GetComponent<HitpointEvent>();
            unitEvent.CallOnHitPointChange(attack.damage);
        }
        
    }*//*
}*/