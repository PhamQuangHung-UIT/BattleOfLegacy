using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class SpellDetailsContent : MonoBehaviour
{
    [SerializeField] GameObject attributeParent;
    [SerializeField] CustomAttributeView attributeViewPrefab;
    [SerializeField] TextMeshProUGUI spellTitleUI, spellLevelTextUI, spellDescriptionUI;
    [SerializeField] Image spellIconUI;
    [SerializeField] TextMeshProUGUI upgradeTitleUI;
    [SerializeField] Button upgradeButton;
    [SerializeField] Image upgradeIcon;
    TextMeshProUGUI upgradeValueUI;
    SpellDetailsSO spellDetails;
    int level;

    GameManagerSettingsSO settings;

    private void Awake()
    {
        upgradeValueUI = upgradeButton.GetComponentInChildren<TextMeshProUGUI>();
        settings = GameManager.Instance.settings;
    }

    public void SetSpellDetail(SpellDetailsSO spellDetails)
    {
        this.spellDetails = spellDetails;
        if (!UpgradeManager.Instance.data.spellLevels.TryGetValue(spellDetails.spellName, out level))
            level = -1;
        upgradeButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.AddListener(Upgrade);
        RenderContent();
    }

    public void RenderContent()
    {
        spellTitleUI.text = spellDetails.spellName;
        spellIconUI.sprite = spellDetails.image;
        spellDescriptionUI.text = spellDetails.spellDescription;

        attributeParent.transform.DetachChildren();

        if (level >= 0)
        {
            spellLevelTextUI.text = "Level " + (level + 1);
            spellLevelTextUI.gameObject.SetActive(true);

            if (level < GameConsts.maxSpellLevel - 1)
            {
                // Set up upgrade fields
                upgradeTitleUI.gameObject.SetActive(true);
                upgradeButton.gameObject.SetActive(true);
                upgradeTitleUI.text = "Upgrade";
                upgradeValueUI.text = $"{spellDetails.spellUpgradeList[level + 1].cost:g}";
                upgradeValueUI.color = spellDetails.spellUpgradeList[level + 1].isGemUpgrade ? settings.gemValueColor : settings.goldValueColor;
                upgradeIcon.sprite = spellDetails.spellUpgradeList[level + 1].isGemUpgrade ? settings.gemIcon : settings.goldIcon;
            }
            else
            {
                upgradeTitleUI.gameObject.SetActive(false);
                upgradeButton.gameObject.SetActive(false);
            }

            // Set up attributes
            foreach (var attribute in spellDetails.spellUpgradeList[level].attributeList)
            {
                CustomAttributeView attributeView = Instantiate(attributeViewPrefab, attributeParent.transform);
                attributeView.SetUp(attribute.name, attribute.value, attribute.valueType, attribute.icon);
                attributeView.SetColor(settings.maxHealthAttributeColor);
            }

        }
        else
        {
            spellLevelTextUI.gameObject.SetActive(false);

            upgradeTitleUI.gameObject.SetActive(true);
            upgradeButton.gameObject.SetActive(true);
            upgradeTitleUI.text = "Buy";
            upgradeValueUI.text = $"{spellDetails.spellUpgradeList[0].cost:g}";
            upgradeValueUI.color = spellDetails.spellUpgradeList[0].isGemUpgrade ? settings.gemValueColor : settings.goldValueColor;
            upgradeIcon.sprite = spellDetails.spellUpgradeList[0].isGemUpgrade ? settings.gemIcon : settings.goldIcon;
        }
    }

    public void Upgrade()
    {
        level++;
        UpgradeManager.Instance.UpgradeUnit(spellDetails.spellName, level);
        SpellDetailsSO.SpellAttributes upgradeDetails = spellDetails.spellUpgradeList[level];
        if (upgradeDetails.isGoldUpgrade)
        {
            GameManager.Instance.playerData.currentGold -= upgradeDetails.cost;
        }
        else
        {
            GameManager.Instance.playerData.currentGem -= upgradeDetails.cost;
        }

        GameManager.Instance.SaveData();
        RenderContent();
    }
}