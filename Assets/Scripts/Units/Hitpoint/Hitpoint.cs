using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(UnitEvent))]
[RequireComponent(typeof(HitpointEvent))]
[DisallowMultipleComponent]
class Hitpoint : MonoBehaviour
{
    public HealthBarUI healthBar;

    private Unit unit;
    private float currentShield;
    private float lastDamageTaken;
    private UnitEvent unitEvent;
    private HitpointEvent hitpointEvent;

    private void Awake()
    {
        unit = GetComponent<Unit>();
        unitEvent = GetComponent<UnitEvent>();
        hitpointEvent = GetComponent<HitpointEvent>();
    }

    public void OnEnable()
    {
        healthBar.Initialized(unit.isEnemy);
        hitpointEvent.OnHitpointChange += HitpointEvent_OnHitpointChange;
        unitEvent.OnShieldAdded += HitpointEvent_OnShieldAdded;
        unitEvent.OnShieldRemoved += HitpointEvent_OnShieldRemoved;
        unitEvent.OnDeath += UnitEvent_OnDeath;
    }

    public void OnDisable()
    {
        hitpointEvent.OnHitpointChange -= HitpointEvent_OnHitpointChange;
        unitEvent.OnShieldAdded -= HitpointEvent_OnShieldAdded;
        unitEvent.OnShieldRemoved -= HitpointEvent_OnShieldRemoved;
        unitEvent.OnDeath -= UnitEvent_OnDeath;
    }

    private void HitpointEvent_OnHitpointChange(HitpointArgs args)
    {
        bool isHurt = false;
        float hitpointValue = ApplyHitpointEffect(args.value);
        if (currentShield == 0 || currentShield > 0 && hitpointValue > 0)
        {
            isHurt = RandomizeHurtState(hitpointValue);
            unit.currentHealth = Mathf.Clamp(unit.currentHealth + hitpointValue, 0, unit.maxHealth);
        }
        else // Damage
        {
            lastDamageTaken += hitpointValue;
            unit.currentHealth += hitpointValue;
        }
        healthBar.SetValue(unit.currentHealth / unit.maxHealth);

        if (unit.currentHealth <= 0)
        {
            unitEvent.CallOnDead();
            unit.isDead = true;
            healthBar.gameObject.SetActive(false);
        }
        else
        {
            if (isHurt)
                unitEvent.CallOnHurt();
            healthBar.gameObject.SetActive(true);
        }
    }

    private bool RandomizeHurtState(float hitpointValue)
    {
        float rand = Random.Range(0f, 1f);
        float max = hitpointValue / unit.maxHealth * 1.5f;
        return max > rand;
    }

    private void HitpointEvent_OnShieldAdded(HitpointArgs args)
    {
        float value = args.value;

        // Reset last damager taken if shield is added
        if (currentShield == 0)
            lastDamageTaken = 0;
        currentShield += value;

    }

    private void HitpointEvent_OnShieldRemoved(HitpointArgs args)
    {
        float value = args.value;
        float removeAmount = value - lastDamageTaken;
        if (removeAmount > 0)
            currentShield -= removeAmount;
        else lastDamageTaken = -removeAmount;
    }

    private void UnitEvent_OnDeath(EventArgs args)
    {
        healthBar.gameObject.SetActive(false);
    }


    /// <summary>
    /// Calculate the final value of the hitpoint value after apply those hitpoint effects
    /// </summary>
    /// <param name="value">The hitpoint value</param>
    /// <returns></returns>
    private float ApplyHitpointEffect(float value)
    {
        bool isDamage = value < 0;
        if (isDamage)
        {
            value += unit.GetEffectValue<ReceiveDamageEffectSO>(ValueType.Absolute);
            value += unit.GetEffectValue<ReceiveDamageEffectSO>(ValueType.Percentage) * value / 100;
        }
        else
        {
            value += unit.GetEffectValue<HealEffectSO>(ValueType.Absolute);
            value += unit.GetEffectValue<HealEffectSO>(ValueType.Percentage) * value / 100;
        }

        if (isDamage && value > 0 || !isDamage && value < 0)
            return 0;
        return value;
    }
    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        ValidateUtilities.AssertNotNull(healthBar, "healthBar");
    }
#endif
    #endregion
}