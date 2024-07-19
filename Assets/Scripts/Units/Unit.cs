using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Required Components
[RequireComponent(typeof(UnitAI))]
[RequireComponent(typeof(Hitpoint))]
[RequireComponent(typeof(HitpointEvent))]
[RequireComponent(typeof(UnitEvent))]
#endregion
[DisallowMultipleComponent]
public class Unit : MonoBehaviour
{
    public UnitBaseStatsSO unitDetails;
    public int currentUnitLevel;
    public bool isEnemy;
    public bool isDead;
    [HideInInspector] public float maxHealth;
    [HideInInspector] public float baseDamage;
    [HideInInspector] public float currentHealth;
    [HideInInspector] public float currentBaseDamage;
    [HideInInspector] public float currentAttackSpeed;
    [HideInInspector] public float currentMovementSpeed;
    [HideInInspector] public List<EffectSO> effectList = new();

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialized(UnitBaseStatsSO stats, int unitLevel, bool isEnemy)
    {
        this.isEnemy = isEnemy;
        unitDetails = stats;
        maxHealth = stats.upgradeDetails[unitLevel].unitMaxHealth;
        baseDamage = stats.upgradeDetails[unitLevel].unitDamage;
        currentHealth = maxHealth;
        currentBaseDamage = baseDamage;
        currentAttackSpeed = stats.attackSpeed;
        currentMovementSpeed = stats.movementSpeed;
        currentUnitLevel = unitLevel;
        isDead = false;
    }

    private void OnEnable()
    {
        gameObject.layer = isEnemy ? LayerMask.NameToLayer("Enemy") : LayerMask.NameToLayer("Player");
        spriteRenderer.flipX = isEnemy;
    }

    private void OnDisable()
    {
        effectList.Clear();
    }

    public float GetEffectValue<T>(ValueType valueType) where T: EffectSO
    {
        float res = 0;
        foreach (var e in effectList)
        {
            if (e is T && e.valueType == valueType)
            {
                res += e.amount / 100f;
            }
        }
        return res;
    }

    #region UNITY_EDITOR
#if UNITY_EDITOR
    private void OnValidate()
    {
        ValidateUtilities.AssertNotNull(unitDetails, "unitDetails");
    }
#endif
    #endregion
}
