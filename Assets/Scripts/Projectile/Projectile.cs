using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Projectile : MonoBehaviour
{
    float damage = 0;
    ProjectileSO projectileDetails;
    Animator anim;
    SpriteRenderer spriteRenderer;
    BoxCollider2D col;
    float remainTraversal;
    bool isEnemyProjectile;
    bool hitTarget;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialized(Unit owner, float damage, ProjectileSO projectileDetails)
    {
        float height = owner.GetComponent<BoxCollider2D>().size.y;
        transform.position = owner.transform.position + new Vector3(0, height * owner.unitDetails.castProjectileRelativePosition, 0);
        spriteRenderer.flipX = owner.isEnemy;
        this.damage = damage;
        isEnemyProjectile = owner.isEnemy;
        this.projectileDetails = projectileDetails;
        anim.runtimeAnimatorController = projectileDetails.animatorController;
        remainTraversal = projectileDetails.maxProjectileRange;
        col.size = projectileDetails.size;
        col.offset = new(0, projectileDetails.size.y / 2);
        hitTarget = false;
    }

    private void OnEnable()
    {
        if (anim.runtimeAnimatorController != null)
        {
            anim.ResetTrigger("Hit");
            anim.SetTrigger("Move");
        }
    }

    private void Update()
    {
        Vector3 movement = new(projectileDetails.speed * Time.deltaTime, 0, 0);
        remainTraversal -= movement.magnitude;
        if (remainTraversal > 0 && (projectileDetails.type == ProjectileType.Penetrate || !hitTarget))
        {
            transform.Translate(movement);
            Debug.Log("Projectile pos:" + transform.position);
        }
        else if (remainTraversal <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator SelfDestroy(float delta)
    {
        yield return new WaitForSeconds(delta);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var targetLayer = LayerMask.NameToLayer(isEnemyProjectile ? "Player" : "Enemy");
        if ((collision.gameObject.layer & targetLayer) != 0 && (projectileDetails.type == ProjectileType.Penetrate || !hitTarget))
        {
            hitTarget = true;
            DealDamage(collision.gameObject);
            if (projectileDetails.type == ProjectileType.SingleTarget)
            {
                float selfDestroyInterval = 0;
                if (projectileDetails.splashRadius > 0)
                {
                    selfDestroyInterval = 0.5f;
                    Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, projectileDetails.splashRadius, 1 << targetLayer);
                    Debug.Log("Projectile hit an area that has: " + hitColliders.Length + " enemies");
                    foreach (Collider2D hitCollider in hitColliders)
                    {
                        if (hitCollider != collision)
                        {
                            DealDamage(hitCollider.gameObject);
                        }
                    }
                }
                anim.ResetTrigger("Move");
                anim.SetTrigger("Hit");
                StartCoroutine(SelfDestroy(selfDestroyInterval));
            }
        }
    }

    private void DealDamage(GameObject target)
    {
        var hitpointEvent = target.GetComponent<HitpointEvent>();
        hitpointEvent.CallOnHitPointChange(-damage);
        if (projectileDetails.attachedEffect != null && target.TryGetComponent<Unit>(out var unit))
        {
            projectileDetails.attachedEffect.ApplyEffect(unit);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (col != null)
        {
            Gizmos.DrawCube(transform.position, col.size);
            if (hitTarget && projectileDetails.splashRadius > 0)
            {
                Gizmos.DrawSphere(transform.position, projectileDetails.splashRadius);
            }
        }
    }
#endif
}