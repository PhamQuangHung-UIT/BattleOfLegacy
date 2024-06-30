using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class GridViewItemGenerator : MonoBehaviour
{
    public GridViewItem gridViewItemPrefab;
    public GridViewType gridViewType;
    public GameObject contentView;

    void Start()
    {
        switch (gridViewType)
        {
            case GridViewType.Unit:
                foreach (var unitDetails in GameManager.Instance.settings.allObtainableUnit)
                {
                    CreateGridItem(unitDetails.image, UpgradeManager.Instance.data.unitLevels.ContainsKey(unitDetails.name), () => OnClickItem(unitDetails));
                }
                break;
            case GridViewType.Spell:
                foreach (var spell in GameManager.Instance.settings.allObtainableSpell)
                {
                    var spellDetails = spell.spellDetails;
                    CreateGridItem(spellDetails.image, UpgradeManager.Instance.data.spellLevels.ContainsKey(spellDetails.spellName), () => OnClickItem(spellDetails));
                }
                break;
            default:
                foreach (var attribute in UpgradeManager.Instance.statueUpgradeDetails.attributeList)
                {
                    CreateGridItem(attribute.Value.icon, UpgradeManager.Instance.data.statueAttributeLevels.ContainsKey(attribute.Key), () => OnClickItem(attribute.Value));
                }
                break;
        }
    }

    private void OnClickItem(UnitBaseStatsSO unitDetails)
    {
        contentView.SetActive(true);
        contentView.GetComponent<UnitDetailsContent>().SetUnitDetail(unitDetails);
    }

    private void OnClickItem(SpellDetailsSO spellDetails)
    {
        contentView.SetActive(true);
        contentView.GetComponent<SpellDetailsContent>().SetSpellDetail(spellDetails);
    }

    private void OnClickItem(StatueAttributeUpgradeSO.StatueAttribute attribute)
    {
        contentView.SetActive(true);
        contentView.GetComponent<StatueAttributeContent>().SetStatueAttributeDetail(attribute);
    }

    void CreateGridItem(Sprite icon, bool hasUnlocked, UnityAction onClick)
    {
        GridViewItem gridViewItem = Instantiate(gridViewItemPrefab, transform);
        gridViewItem.icon.sprite = icon;
        gridViewItem.button.onClick.AddListener(onClick);
        if (hasUnlocked)
        {
            gridViewItem.cover.color = GameConsts.transparent;
        } else gridViewItem.cover.color = GameManager.Instance.settings.disableColorForCover;
    }
}