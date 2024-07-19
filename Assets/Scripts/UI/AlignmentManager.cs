using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class AlignmentManager : MonoBehaviour
{
    public GridViewItem gridViewItemPrefab;
    public GameObject unitAlignmentUI, spellAlignmentUI, unitGridView, spellGridView;
    List<UnitBaseStatsSO> unitAlignment;
    List<SpellBase> spellAlignment;
    AlignmentSlot[] unitAlignmentSlots, spellAlignmentSlots;
    Dictionary<string, GridViewItem> gridItemDict = new();
    Dictionary<string, GridViewItem> availableSpellDict = new();

    int maxSpellSlot;

    private void Awake()
    {
        unitAlignment = new(GameManager.Instance.playerData.playerAlignment.unitAlignment);
        spellAlignment = new(GameManager.Instance.playerData.playerAlignment.spellAlignment);
        unitAlignmentSlots = unitAlignmentUI.GetComponentsInChildren<AlignmentSlot>();
        spellAlignmentSlots = spellAlignmentUI.GetComponentsInChildren<AlignmentSlot>();
        maxSpellSlot = (int)UpgradeManager.Instance.statueUpgradeDetails.maxSpellSlotPerLevelDetails[UpgradeManager.Instance.data.statueAttributeLevels["maxSpellSlot"]].amount;

        foreach (var u in GameManager.Instance.settings.allObtainableUnit)
        {
            var unitDetails = u;
            if (UpgradeManager.Instance.data.unitLevels.ContainsKey(unitDetails.unitName))
                CreateGridViewItem(unitDetails.unitName,
                                       unitDetails.image,
                                       () => OnClickItem(unitDetails), GridViewType.Unit);
        }
        foreach (var s in GameManager.Instance.settings.allObtainableSpell)
        {
            var spell = s;
            if (UpgradeManager.Instance.data.spellLevels.ContainsKey(spell.spellDetails.spellName))
                CreateGridViewItem(spell.spellDetails.spellName,
                                    spell.spellDetails.image,
                                    () => OnClickItem(spell), GridViewType.Spell);
        }

        // Disable unused spell slots
        for (int i = maxSpellSlot; i < spellAlignmentSlots.Length; i++)
        {
            spellAlignmentSlots[i].gameObject.SetActive(false);
        }

        RenderSlots();
    }

    private bool OnClickItem(UnitBaseStatsSO unitDetails)
    {
        if (!unitAlignment.Exists(u => u == unitDetails) && unitAlignment.Count < 10)
        {
            unitAlignment.Add(unitDetails);
            RenderSlots();
            return true;
        }
        return false;
    }

    private bool OnClickItem(SpellBase spell)
    {
        if (spellAlignment.Count < maxSpellSlot && !spellAlignment.Exists(s => s = spell))
        {
            spellAlignment.Add(spell);
            RenderSlots();
            return true;
        }
        return false;
    }

    private void RenderSlots()
    {
        for (int i = 0; i < unitAlignmentSlots.Length; i++)
        {
            if (i < unitAlignment.Count)
            {
                int index = i;
                unitAlignmentSlots[i].SetItem(unitAlignment[i].image);
                unitAlignmentSlots[i].button.onClick.RemoveAllListeners();
                unitAlignmentSlots[i].button.onClick.AddListener(() => RemoveItem(unitAlignment[index]));
            } else
            {
                unitAlignmentSlots[i].RemoveItem();
            }
        }

        for (int i = 0; i < maxSpellSlot; i++)
        {
            if (i < spellAlignment.Count)
            {
                int index = i;
                spellAlignmentSlots[i].SetItem(spellAlignment[i].spellDetails.image);
                spellAlignmentSlots[i].button.onClick.RemoveAllListeners();
                spellAlignmentSlots[i].button.onClick.AddListener(() => RemoveItem(spellAlignment[index]));
            } else
            {
                spellAlignmentSlots[i].RemoveItem();
            }
        }
    }

    private void RemoveItem(UnitBaseStatsSO unitDetails)
    {
        var gridViewItem = gridItemDict[unitDetails.unitName];
        gridItemDict.Remove(unitDetails.unitName);
        gridViewItem.cover.color = new();
        unitAlignment.Remove(unitDetails);
        RenderSlots();
    }

    private void RemoveItem(SpellBase spell)
    {
        var gridViewItem = gridItemDict[spell.spellDetails.spellName];
        gridItemDict.Remove(spell.spellDetails.spellName);
        gridViewItem.cover.color = new();
        spellAlignment.Remove(spell);
        RenderSlots();
    }

    void CreateGridViewItem(string name, Sprite icon, Func<bool> onClick, GridViewType type)
    {
        GridViewItem gridViewItem = Instantiate(gridViewItemPrefab,
            type == GridViewType.Unit ? unitGridView.transform : spellGridView.transform);
        gridViewItem.icon.sprite = icon;
        gridViewItem.button.onClick.AddListener(() =>
        {
            if (onClick.Invoke())
                AddGridViewItem(name, gridViewItem);
        });
        if (unitAlignment.Exists(ud => ud.unitName == name) || spellAlignment.Exists(s => s.spellDetails.spellName == name))
        {
            AddGridViewItem(name, gridViewItem);
        }
    }

    private void AddGridViewItem(string name, GridViewItem gridViewItem)
    {
        gridItemDict.Add(name, gridViewItem);
        gridViewItem.cover.color = GameManager.Instance.settings.disableColorForCover;
    }

    // Save player's alignment
    public void Save()
    {
        if (unitAlignment.Count == 0 || spellAlignment.Count == 0) return;
        GameManager.Instance.playerData.playerAlignment.unitAlignment = unitAlignment;
        GameManager.Instance.playerData.playerAlignment.spellAlignment = spellAlignment;
        GameManager.Instance.SaveData();
        Close();
    }

    // Close the build scene
    public void Close()
    {
        GameManager.Instance.GoToSelectLevel();
    }
}
