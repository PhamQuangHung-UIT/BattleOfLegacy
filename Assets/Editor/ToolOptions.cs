using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ToolOptions
{
    [MenuItem("Tools/Clear PersistentStorage")]
    public static void ClearPersistentStorage()
    {
        List<string> paths = new(Directory.GetFiles(Application.persistentDataPath));
        paths.AddRange(Directory.GetDirectories(Application.persistentDataPath));
        foreach (string path in paths)
        {
            Directory.Delete(path, true);
        }
    }

    [MenuItem("Tools/Clear PlayerPrefs")]
    public static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
