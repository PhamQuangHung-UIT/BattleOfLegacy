using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class StatueAttributeContent : MonoBehaviour
{
    [SerializeField] GameObject attributeParent;
    [SerializeField] CustomAttributeView attributeViewPrefab;
    [SerializeField] TextMeshProUGUI statueAttributeTitleUI, statueAttributeLevelTextUI, statueAttributeDescriptionUI;
    [SerializeField] Image statueAttributeIconUI;
    [SerializeField] TextMeshProUGUI upgradeTitleUI;
    [SerializeField] Button upgradeButton;
    [SerializeField] Image upgradeIcon;
    TextMeshProUGUI upgradeValueUI;
    StatueAttributeUpgradeSO.StatueAttribute statueAttributeDetails;
    int level;

    GameManagerSettingsSO settings;

    private void Awake()
    {
        upgradeValueUI = upgradeButton.GetComponentInChildren<TextMeshProUGUI>();
        settings = GameManager.Instance.settings;
    }

    public void SetStatueAttributeDetail(StatueAttributeUpgradeSO.StatueAttribute statueAttributeDetails)
    {
        this.statueAttributeDetails = statueAttributeDetails;
        if (!UpgradeManager.Instance.data.statueAttributeLevels.TryGetValue(statueAttributeDetails.name, out level))
            level = -1;
        upgradeButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.AddListener(Upgrade);
        RenderContent();
    }

    public void RenderContent()
    {
        statueAttributeTitleUI.text = statueAttributeDetails.upgradeTitle;
        statueAttributeIconUI.sprite = statueAttributeDetails.icon;
        statueAttributeDescriptionUI.text = statueAttributeDetails.description;

        foreach (Transform obj in attributeParent.transform)
            Destroy(obj.gameObject);

        if (level >= 0)
        {
            statueAttributeLevelTextUI.text = "Level " + (level + 1);
            statueAttributeLevelTextUI.gameObject.SetActive(true);

            if (level < statueAttributeDetails.upgradeDetails.Length - 1)
            {
                // Set up upgrade fields
                upgradeTitleUI.gameObject.SetActive(true);
                upgradeButton.gameObject.SetActive(true);
                upgradeTitleUI.text = "Upgrade";
                upgradeValueUI.text = $"{statueAttributeDetails.upgradeDetails[level + 1].upgradeCost:g}";
                upgradeValueUI.color = statueAttributeDetails.upgradeDetails[level + 1].isGemUpgrade ? settings.gemValueColor : settings.goldValueColor;
                upgradeIcon.sprite = statueAttributeDetails.upgradeDetails[level + 1].isGemUpgrade ? settings.gemIcon : settings.goldIcon;
            }
            else
            {
                upgradeTitleUI.gameObject.SetActive(false);
                upgradeButton.gameObject.SetActive(false);
            }

            // Set up attributes
            CustomAttributeView attributeView = Instantiate(attributeViewPrefab, attributeParent.transform);
            attributeView.SetUp(statueAttributeDetails.title, statueAttributeDetails.upgradeDetails[level].amount, statueAttributeDetails.valueType, statueAttributeDetails.icon);
            attributeView.SetColor(settings.statueAttributeColor);

        }
        else
        {
            statueAttributeLevelTextUI.gameObject.SetActive(false);

            upgradeTitleUI.gameObject.SetActive(true);
            upgradeButton.gameObject.SetActive(true);
            upgradeTitleUI.text = "Buy";
            upgradeValueUI.text = $"{statueAttributeDetails.upgradeDetails[0].upgradeCost:g}";
            upgradeValueUI.color = statueAttributeDetails.upgradeDetails[0].isGemUpgrade ? settings.gemValueColor : settings.goldValueColor;
            upgradeIcon.sprite = statueAttributeDetails.upgradeDetails[0].isGemUpgrade ? settings.gemIcon : settings.goldIcon;
            upgradeIcon.sprite = statueAttributeDetails.upgradeDetails[0].isGemUpgrade ? settings.gemIcon : settings.goldIcon;
        }
    }

    public void Upgrade()
    {
        level++;
        UpgradeManager.Instance.UpgradeStatueAttribute(statueAttributeDetails.name, level);
        StatueAttributeUpgradeSO.StatueAttributeUpgrade upgradeDetails = statueAttributeDetails.upgradeDetails[level];
        if (upgradeDetails.isGemUpgrade)
        {
            GameManager.Instance.playerData.currentGem -= upgradeDetails.upgradeCost;
        }
        else
        {
            GameManager.Instance.playerData.currentGold -= upgradeDetails.upgradeCost;
        }

        GameManager.Instance.SaveData();
        RenderContent();
    }
}