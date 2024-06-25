using UnityEngine;

[RequireComponent(typeof(HitpointEvent))]
[RequireComponent(typeof(BoxCollider2D))]
[DisallowMultipleComponent]
public class Statue : MonoBehaviour
{
    [HideInInspector] public float maxHealth;
    float currentHealth;
    HitpointEvent hitpointEvent;

    private void Awake()
    {
        if ((gameObject.layer & LayerMask.NameToLayer("Enemy")) != 0)
        {
            maxHealth = Level.Instance.levelDetails.enemyStatueMaxHealth;
        }
        else
        {
            int maxHealthLevel = UpgradeManager.Instance.data.statueAttributeLevels["maxHealth"];
            maxHealth = UpgradeManager.Instance.statueUpgradeDetails.statueHealthPerLevelDetails[maxHealthLevel].amount;
        }
        currentHealth = maxHealth;
        hitpointEvent = GetComponent<HitpointEvent>();
    }

    private void OnEnable()
    {
        hitpointEvent.OnHitpointChange += HitpointEvent_OnHitpointChange;
    }

    private void OnDisable()
    {
        hitpointEvent.OnHitpointChange -= HitpointEvent_OnHitpointChange;
    }

    private void HitpointEvent_OnHitpointChange(HitpointArgs args)
    {
        currentHealth -= args.value;
        if (currentHealth < 0)
        {
            if ((gameObject.layer & LayerMask.NameToLayer("Enemy")) != 0)
            {
                StaticEventHandler.CallOnVictory();
            }
            else
            {
                StaticEventHandler.CallOnDefeat();
            }
        }
    }
}