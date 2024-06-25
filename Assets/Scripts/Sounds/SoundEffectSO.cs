using UnityEngine;

[CreateAssetMenu(fileName = "SoundEffect_", menuName = "Scriptable Objects/Sound/SoundEffect")]
public class SoundEffectSO : ScriptableObject
{
    #region Tooltip
    [Tooltip("Audio clip with sound effect")]
    #endregion
    public AudioClip clip;

    #region Tooltip
    [Tooltip("Sound effect volume. Default to 1")]
    [Range(0f, 1f)]
    #endregion
    public float volume = 1;

    #region Tooltip
    [Tooltip("Sound effect pitch. Default to 1")]
    #endregion
    public float pitch = 1;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        ValidateUtilities.AssertNotNull(clip, "audioClip");
    }
#endif
    #endregion
}