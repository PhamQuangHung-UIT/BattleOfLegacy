using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;

public static class StorageUtils
{
    private const string _default = "Local/";

    public static string persistentPath = Application.persistentDataPath;

    public static void Save(ISerializable data, string filename, string folderName = _default)
    {
        var saveFolderPath = persistentPath + "/" + folderName;
        var saveFilePath = saveFolderPath + filename;

        if (!Directory.Exists(saveFolderPath))
        {
            Directory.CreateDirectory(saveFolderPath);
        }

        FileStream stream = File.Open(saveFilePath, FileMode.OpenOrCreate);

        data.SaveData(stream);
    }

    public static bool Load(ISerializable data, string filename, string folderName = _default)
    {
        var saveFolderPath = persistentPath + "/" + folderName;
        var saveFilePath = saveFolderPath + filename;

        if (!Directory.Exists(saveFolderPath) || !File.Exists(saveFilePath))
        {
            return false;
        }

        using (FileStream stream = File.OpenRead(saveFilePath))
        {
            data.LoadData(stream);
        }

        return true;
    }
} 