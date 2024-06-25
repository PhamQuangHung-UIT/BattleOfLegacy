using System.IO;

public class PlayerGameDataSerializable : ISerializable
{
    public long currentGold = 0;
    public int currentGem = 0;
    public PlayerAlignmentSerializable playerAlignment;

    public void LoadData(Stream stream)
    {
        using var reader = new BinaryReader(stream);
        currentGold = reader.ReadInt64();
        currentGem = reader.ReadInt32();
        reader.Close();
        playerAlignment = new();
        playerAlignment.LoadData(stream);
    }

    public void SaveData(Stream stream)
    {
        using var writer = new BinaryWriter(stream);
        writer.Write(currentGold);
        writer.Write(currentGem);
        writer.Close();
        playerAlignment.SaveData(stream);
    }
}