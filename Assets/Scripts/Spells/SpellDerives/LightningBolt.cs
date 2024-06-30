using System.Collections;
using UnityEngine;

public class LightningBolt : SpellBase
{
    public GameObject lightningPrefab;
    float damage;
    private const float strikeRadius = 8f;

    // Start is called before the first frame update
    void Start()
    {
        damage = spellDetails.spellAttributeMapPerLevel[level]["Damage"];
    }

    public override void Execute()
    {
        StartCoroutine(CastSkill());
    }

    IEnumerator CastSkill()
    {
        float xCenter = Camera.main.transform.position.x;
        float yCenter = Level.Instance.playerSpawnPos.y;
        Vector2 center = new(xCenter, yCenter);
        var lightningObject = Instantiate(lightningPrefab, new Vector3(xCenter, yCenter, -1), Quaternion.identity);
        Destroy(lightningObject, 0.8f);
        yield return new WaitForSeconds(0.8f);
        foreach (var unit in Level.Instance.unitList)
        {
            if (unit.isEnemy && Vector2.Distance(center, unit.transform.position) <= strikeRadius)
            {
                unit.GetComponent<HitpointEvent>().CallOnHitPointChange(-damage);
            }
        }
        Destroy(gameObject);
    }
}
