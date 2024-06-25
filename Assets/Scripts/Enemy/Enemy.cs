using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public EnemyDetailsSO enemyDetails;
    private UnitEvent unitEvent;

    public void Awake()
    {
        unitEvent = GetComponent<UnitEvent>();
    }

    public void Initialized(EnemyDetailsSO enemyDetails)
    {
        this.enemyDetails = enemyDetails;
    }

    public void OnEnable()
    {
        unitEvent.OnDeath += UnitEvent_OnDeath;
    }

    public void OnDisable()
    {
        unitEvent.OnDeath -= UnitEvent_OnDeath;
    }

    private void UnitEvent_OnDeath(EventArgs args)
    {
        GoldDrop goldDrop = PoolManager.Instance.ReuseComponent<GoldDrop>(transform.position, transform.rotation);
        goldDrop.amount = enemyDetails.goldAwarded;
        goldDrop.gameObject.SetActive(true);
    }
}