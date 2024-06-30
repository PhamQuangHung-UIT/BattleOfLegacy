using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Projectile: MonoBehaviour
{
    Vector2 direction;
    float damage = 0;
    ProjectileSO projectileDetails;
    Animator anim;
    SpriteRenderer spriteRenderer;
    BoxCollider2D col;
    Unit lastHitUnit;
    float remainTraversal;
    bool isEnemyProjectile;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialized(Unit owner, float damage, ProjectileSO projectileDetails)
    {
        float yOffset = owner.GetComponent<BoxCollider2D>().offset.y;
        transform.position = owner.transform.position;
        transform.position = transform.position + new Vector3(0, yOffset, 0);
        direction = isEnemyProjectile ? Vector2.left : -Vector2.left;
        spriteRenderer.flipX = isEnemyProjectile;
        this.damage = damage;
        this.isEnemyProjectile = owner.isEnemy;
        this.projectileDetails = projectileDetails;
        anim.runtimeAnimatorController = projectileDetails.animatorController;
        lastHitUnit = null;
        remainTraversal = projectileDetails.maxProjectileRange;
        col.size = projectileDetails.size;
        col.offset = new(0, projectileDetails.size.y / 2);
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
        if (collision.gameObject.TryGetComponent(out Unit unit) && unit.isEnemy ^ isEnemyProjectile && lastHitUnit != unit)
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
                anim.ResetTrigger("Move");
                anim.SetTrigger("Hit");
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
}