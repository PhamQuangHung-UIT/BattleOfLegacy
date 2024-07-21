using System.Collections;
using System.Linq;
using UnityEngine;

public class LightningBolt : SpellBase
{
    public GameObject lightningPrefab;
    float damage;
    private const float strikeRadius = 5f;

    // Start is called before the first frame update
    void Start()
    {
        damage = spellDetails.spellAttributeMapPerLevel[level]["Damage"].value;
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
        Destroy(lightningObject, 0.8f);
        yield return new WaitForSeconds(0.8f);
        /*        foreach (var unit in Level.Instance.unitList.Where(u => u.isEnemy).ToList())
                {
                    if (Mathf.Abs(xCenter - unit.transform.position.x) <= strikeRadius)
                    {
                        unit.GetComponent<HitpointEvent>().CallOnHitPointChange(-damage);
                    }
                }*/
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(new(xCenter, yCenter), strikeRadius, LayerMask.GetMask("Enemy"));
        foreach (var hitTarget in hitTargets)
        {
            if (!hitTarget.gameObject.CompareTag("Statue"))
                hitTarget.gameObject.GetComponent<HitpointEvent>().CallOnHitPointChange(-damage);
        }
        Destroy(gameObject);
    }
}
