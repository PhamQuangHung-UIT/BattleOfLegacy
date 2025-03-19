using System;
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

    UnitEvent unitEvent;
    SpriteRenderer spriteRenderer;
    new BoxCollider2D collider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        unitEvent = GetComponent<UnitEvent>();
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
        collider.enabled = true;
        unitEvent.OnDeath += UnitEvent_OnDeath;
    }

    private void UnitEvent_OnDeath(EventArgs args)
    {
        collider.enabled = false;
    }

    private void OnDisable()
    {
        effectList.Clear();
    }


    // FixedUpdate
    private void FixedUpdate()
    {
        if (spriteRenderer.sprite)
        {
            collider.size = spriteRenderer.sprite.bounds.size * 0.7f;
            collider.offset = new(0, collider.size.y / 2);
        }
    }



    public float GetEffectValue<T>(ValueType valueType) where T : EffectSO
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
