using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
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

    [MenuItem("Tools/Open Scene/Menu")]
    public static void OpenMenuScene()
    {
        // Replace "Assets/Scenes/MyScene.unity" with the path to your scene
        string scenePath = "Assets/Scenes/Main Menu.unity";
        EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
    }

    [MenuItem("Tools/Open Scene/Select Level")]
    public static void OpenSelectLevelScene()
    {
        // Replace "Assets/Scenes/MyScene.unity" with the path to your scene
        string scenePath = "Assets/Scenes/Select Level.unity";
        EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
    }

    [MenuItem("Tools/Open Scene/Level")]
    public static void OpenLevelScene()
    {
        // Replace "Assets/Scenes/MyScene.unity" with the path to your scene
        string scenePath = "Assets/Scenes/Level.unity";
        EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
    }

    [MenuItem("Tools/Open Scene/Upgrade")]
    public static void OpenUpgradeScene()
    {
        // Replace "Assets/Scenes/MyScene.unity" with the path to your scene
        string scenePath = "Assets/Scenes/Upgrade.unity";
        EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
    }

    [MenuItem("Tools/Open Scene/Build")]
    public static void OpenBuildScene()
    {
        // Replace "Assets/Scenes/MyScene.unity" with the path to your scene
        string scenePath = "Assets/Scenes/Build.unity";
        EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
    }
}
