using UnityEngine;

[RequireComponent(typeof(HitpointEvent))]
[RequireComponent(typeof(BoxCollider2D))]
[DisallowMultipleComponent]
public class Statue : MonoBehaviour
{
    public Sprite destroyStatueSprite;
    public GameObject smokeEffect, panelToShow;
    [HideInInspector] public float maxHealth;
    [HideInInspector] public float currentHealth;
    HitpointEvent hitpointEvent;
    SpriteRenderer spriteRenderer;
    HealthBarUI healthBarUI;
    bool isEnemyStatue;

    private void Awake()
    {
        hitpointEvent = GetComponent<HitpointEvent>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthBarUI = GetComponentInChildren<HealthBarUI>();
        healthBarUI.Initialized(isEnemyStatue);
    }

    private void Start()
    {
        if ((gameObject.layer & LayerMask.NameToLayer("Enemy")) != 0)
        {
            maxHealth = Level.Instance.levelDetails.enemyStatueMaxHealth;
        }
        else
        {
            int maxHealthLevel = UpgradeManager.Instance.data.statueAttributeLevels["statueHealth"];
            maxHealth = UpgradeManager.Instance.statueUpgradeDetails.statueHealthPerLevelDetails[maxHealthLevel].amount;
        }
        currentHealth = maxHealth;
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
        currentHealth += args.value;
        healthBarUI.SetValue(currentHealth / maxHealth);
        if (currentHealth < 0)
        {
            spriteRenderer.sprite = destroyStatueSprite;
            smokeEffect.SetActive(true);
            if (isEnemyStatue)
            {
                StaticEventHandler.CallOnVictory();
            }
            else
            {
                StaticEventHandler.CallOnDefeat();
            }
            healthBarUI.gameObject.SetActive(false);
            Invoke(nameof(ShowPanel), 3);
        }
    }

    private void ShowPanel()
    {
        panelToShow.SetActive(true);
    }
}