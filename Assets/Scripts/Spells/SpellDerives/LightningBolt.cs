using System.Collections;
using UnityEngine;

public class LightningBolt : SpellBase
{
    public GameObject lightningPrefab;
    float damage;
    private const float strikeRadius = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        damage = spellDetails.spellAttributeMapPerLevel[level]["damage"];
    }

    public override void Execute()
    {
        StartCoroutine(CastSkill());
    }

    IEnumerator CastSkill()
    {
        float xCenter = Camera.main.transform.position.x;
        float yCenter = Level.Instance.playerSpawnPos.y;
        var lightningObject = Instantiate(lightningPrefab, new Vector3(xCenter, yCenter, -1), Quaternion.identity);
        var lightningAnim = lightningObject.GetComponent<Animator>();
        lightningAnim.SetTrigger("Play");
        yield return new WaitForSeconds(0.8f);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new(xCenter, yCenter), strikeRadius);
        foreach (var collider in colliders)
        {
            if (!collider.CompareTag("Confiner"))
            {
                collider.GetComponent<HitpointEvent>().CallOnHitPointChange(-damage);
            }
        }
    }
}
