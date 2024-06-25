using System.IO;

public interface ISerializable
{
    void LoadData(Stream stream);

    void SaveData(Stream stream);
}