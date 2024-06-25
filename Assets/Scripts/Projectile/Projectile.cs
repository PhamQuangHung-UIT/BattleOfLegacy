using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class Projectile: MonoBehaviour
{
    Vector2 direction;
    HitpointEvent target;
    float damage = 0;
    ProjectileSO projectileDetails;
    Animator anim;
    Unit lastHitUnit;
    float remainTraversal;

    public void Initialized(Vector2 direction, float damage, ProjectileSO projectileDetails)
    {
        this.direction = direction;
        this.damage = damage;
        this.projectileDetails = projectileDetails;
        anim = GetComponent<Animator>();
        anim.runtimeAnimatorController = projectileDetails.animatorController;
        lastHitUnit = null;
        remainTraversal = projectileDetails.maxProjectileRange;
    }

    private void OnEnable()
    {
        anim.ResetTrigger("Hit");
        anim.SetTrigger("Move");
    }

    private void Update()
    {
        Vector3 movement = projectileDetails.speed * Time.deltaTime * direction;
        remainTraversal -= movement.magnitude;
        if (remainTraversal > 0 && (projectileDetails.type == ProjectileType.Penetrate
            || projectileDetails.type == ProjectileType.SingleTarget
            && !lastHitUnit))
        {
            transform.Translate(movement);
        }
    }

    private IEnumerator SelfDestroy(float delta)
    {
        yield return new WaitForSeconds(delta);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Unit unit) && lastHitUnit != unit)
        {
            lastHitUnit = unit;
            DealDamage(unit);
            if (projectileDetails.type == ProjectileType.SingleTarget)
            {
                if (projectileDetails.splashRadius > 0)
                {
                    Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, projectileDetails.splashRadius);
                    foreach (Collider2D hitCollider in hitColliders)
                    {
                        Unit affectedUnit = hitCollider.gameObject.GetComponent<Unit>();
                        DealDamage(affectedUnit);
                    }

                }
                StartCoroutine(SelfDestroy(0.25f));
            }
        }
    }

    private void DealDamage(Unit unit)
    {
        var hitpointEvent = unit.GetComponent<HitpointEvent>();
        hitpointEvent.CallOnHitPointChange(-damage);
        if (projectileDetails.attachedEffect != null)
        {
            projectileDetails.attachedEffect.ApplyEffect(unit);
        }
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        ValidateUtilities.Assert(target != null ^ direction != null);
    }
#endif
    #endregion
}