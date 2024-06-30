using System.IO;

public class LevelManagerSerializableData : ISerializable
{
    public int selectedLevel;
    public int currentLevel;
    public void LoadData(Stream stream)
    {
        using BinaryReader br = new(stream);
        selectedLevel = br.ReadInt32();
        currentLevel = br.ReadInt32();
    }

    public void SaveData(Stream stream)
    {
        using BinaryWriter br = new(stream);
        br.Write(selectedLevel);
        br.Write(currentLevel);
    }
}