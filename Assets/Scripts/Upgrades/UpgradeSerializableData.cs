using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class UpgradeSerializableData : ISerializable
{
    public Dictionary<string, int> unitLevels = new();

    public Dictionary<string, int> spellLevels = new();

    public Dictionary<string, int> statueAttributeLevels;

    public UpgradeSerializableData()
    {
        statueAttributeLevels = new();
        foreach (var unitDetail in GameManager.Instance.settings.beginUnitAlignment)
        {
            unitLevels.Add(unitDetail.unitName, 0);
        }

        foreach (var spell in GameManager.Instance.settings.beginSpellAlignment)
        {
            spellLevels.Add(spell.spellDetails.spellName, 0);
        }

        foreach (var key in UpgradeManager.Instance.statueUpgradeDetails.attributeList.Keys)
        {
            statueAttributeLevels.Add(key, 0);
        }
    }

    public void LoadData(Stream stream)
    {
        using BinaryReader reader = new(stream);
        int count = reader.ReadInt32();
        unitLevels.Clear();
        for (int i = 0; i < count; i++)
        {
            string name = reader.ReadString();
            int level = reader.ReadInt32();
            Debug.Log($"{name}: {level}");
            unitLevels.Add(name, level);
        }
        count = reader.ReadInt32();
        spellLevels.Clear();
        for (int i = 0; i < count; i++)
        {
            string attributeName = reader.ReadString();
            int level = reader.ReadInt32();
            spellLevels.Add(attributeName, level);
        }
        count = reader.ReadInt32();
        statueAttributeLevels.Clear();
        for (int i = 0; i < count; i++)
        {
            string attributeName = reader.ReadString();
            int level = reader.ReadInt32();
            statueAttributeLevels.Add(attributeName, level);
        }
    }

    public void SaveData(Stream stream)
    {
        using BinaryWriter writer = new(stream);
        writer.Write(unitLevels.Count);
        foreach (var unit in unitLevels)
        {
            writer.Write(unit.Key); 
            writer.Write(unit.Value);
        }
        writer.Write(spellLevels.Count);
        foreach (var spell in spellLevels)
        {
            writer.Write(spell.Key);
            writer.Write(spell.Value);
        }
        writer.Write(statueAttributeLevels.Count);
        foreach (var attribute in statueAttributeLevels)
        {
            writer.Write(attribute.Key);
            writer.Write(attribute.Value);
        }
    }
}