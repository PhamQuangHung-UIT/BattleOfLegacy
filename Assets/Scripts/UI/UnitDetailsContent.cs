using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class UnitDetailsContent: MonoBehaviour
{
    [SerializeField] GameObject attributeParent;
    [SerializeField] CustomAttributeView attributeViewPrefab;
    [SerializeField] TextMeshProUGUI unitTitleUI, unitLevelTextUI, unitDescriptionUI;
    [SerializeField] Image unitIconUI;
    [SerializeField] TextMeshProUGUI upgradeTitleUI;
    [SerializeField] Button upgradeButton;
    [SerializeField] Image upgradeIcon;
    TextMeshProUGUI upgradeValueUI;
    UnitBaseStatsSO unitDetail;
    int level;

    GameManagerSettingsSO settings;

    private void Awake()
    {
        upgradeValueUI = upgradeButton.GetComponentInChildren<TextMeshProUGUI>();
        settings = GameManager.Instance.settings;
    }

    public void SetUnitDetail(UnitBaseStatsSO unitDetail)
    {
        this.unitDetail = unitDetail;
        if (!UpgradeManager.Instance.data.unitLevels.TryGetValue(unitDetail.unitName, out level))
            level = -1;
        upgradeButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.AddListener(Upgrade);
        RenderContent();
    }

    public void RenderContent()
    {
        unitTitleUI.text = unitDetail.unitName;
        unitDescriptionUI.text = unitDetail.unitDescription;
        unitIconUI.sprite = unitDetail.image;

        attributeParent.transform.DetachChildren();

        if (level >= 0)
        {
            unitLevelTextUI.text = "Level " + (level + 1);
            unitLevelTextUI.gameObject.SetActive(true);

            if (level < GameConsts.maxUnitLevel - 1)
            {
                // Set up upgrade fields
                upgradeTitleUI.gameObject.SetActive(true);
                upgradeButton.gameObject.SetActive(true);
                upgradeTitleUI.text = "Upgrade";
                upgradeValueUI.text = $"{unitDetail.upgradeDetails[level + 1].cost:g}";
                upgradeValueUI.color = unitDetail.upgradeDetails[level + 1].isGemUpgrade ? settings.gemValueColor : settings.goldValueColor;
                upgradeIcon.sprite = unitDetail.upgradeDetails[level + 1].isGemUpgrade ? settings.gemIcon : settings.goldIcon;
            } else
            {
                upgradeTitleUI.gameObject.SetActive(false);
                upgradeButton.gameObject.SetActive(false);
            }

            // Set up attributes
            CustomAttributeView attributeView = Instantiate(attributeViewPrefab, attributeParent.transform);
            attributeView.SetUp("Max Health", unitDetail.upgradeDetails[level].unitMaxHealth, ValueType.Absolute,settings.healthIcon);
            attributeView.SetColor(settings.maxHealthAttributeColor);

            attributeView = Instantiate(attributeViewPrefab, attributeParent.transform);
            attributeView.SetUp("Damage", unitDetail.upgradeDetails[level].unitDamage, ValueType.Absolute, settings.attackIcon);
            attributeView.SetColor(settings.attackDamageAttributeColor);

            attributeView = Instantiate(attributeViewPrefab, attributeParent.transform);
            attributeView.SetUp("Attack Range", unitDetail.attackRange, ValueType.Absolute, settings.attackRangeIcon);
            attributeView.SetColor(settings.defaultAttributeColor);

            /*attributeView = Instantiate(attributeViewPrefab, attributeParent.transform);
            attributeView.SetUp("Attack Speed", unitDetail.attackSpeed, null);
            attributeView.SetColor(settings.defaultAttributeColor);*/

            attributeView = Instantiate(attributeViewPrefab, attributeParent.transform);
            attributeView.SetUp("Speed", unitDetail.movementSpeed, ValueType.Absolute, settings.speedIcon);
            attributeView.SetColor(settings.defaultAttributeColor);
        } else
        {
            unitLevelTextUI.gameObject.SetActive(false);

            upgradeTitleUI.gameObject.SetActive(true);
            upgradeButton.gameObject.SetActive(true);
            upgradeTitleUI.text = "Buy";
            upgradeValueUI.text = $"{unitDetail.upgradeDetails[0].cost:g}";
            upgradeValueUI.color = unitDetail.upgradeDetails[0].isGemUpgrade ? settings.gemValueColor : settings.goldValueColor;
            upgradeIcon.sprite = unitDetail.upgradeDetails[0].isGemUpgrade ? settings.gemIcon : settings.goldIcon;
        }
    }

    public void Upgrade()
    {
        UnitUpgradeDetails upgradeDetails = unitDetail.upgradeDetails[level + 1];
        if (upgradeDetails.isGoldUpgrade && GameManager.Instance.playerData.currentGold > upgradeDetails.cost)
        {
            GameManager.Instance.playerData.currentGold -= upgradeDetails.cost;
            level++;
            UpgradeManager.Instance.UpgradeUnit(unitDetail.unitName, level);
        } else if (GameManager.Instance.playerData.currentGem > upgradeDetails.cost)
        {
            GameManager.Instance.playerData.currentGem -= upgradeDetails.cost;
            level++;
            UpgradeManager.Instance.UpgradeUnit(unitDetail.unitName, level);
        }

        GameManager.Instance.SaveData();
        RenderContent();
    }
}