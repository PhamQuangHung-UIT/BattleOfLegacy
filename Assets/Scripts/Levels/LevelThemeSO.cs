using UnityEngine;

[CreateAssetMenu(fileName = "LevelTheme_", menuName = "Scriptable Objects/Level/LevelTheme")]
public class LevelThemeSO : ScriptableObject
{
    #region Tooltip
    [Tooltip("The main theme of the level")]
    #endregion
    public Theme theme;

    #region Tooltip
    [Tooltip("The background music associate with the theme")]
    #endregion
    public AudioClip backgroundMusic;
}