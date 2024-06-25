using System.Collections.Generic;
using System.IO;

public class UpgradeSerializableData : ISerializable
{
    public Dictionary<string, int> unitLevels = new();

    public Dictionary<string, int> statueAttributeLevels = new();

    public void LoadData(Stream stream)
    {
        using BinaryReader reader = new(stream);
        int count = reader.ReadInt32();
        for (int i = 0; i < count; i++)
        {
            string name = reader.ReadString();
            int level = reader.ReadInt32();
            unitLevels.Add(name, level);
        }
        count = reader.ReadInt32();
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
        writer.Write(statueAttributeLevels.Count);
        foreach (var attribute in statueAttributeLevels)
        {
            writer.Write(attribute.Key);
            writer.Write(attribute.Value);
        }
    }
}