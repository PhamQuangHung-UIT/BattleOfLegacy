using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    private const string sfxVolumeKey = "sfxVolume";
    private const string musicVolumeKey = "musicVolume";
    public float sfxVolume;

    AudioSource musicAudioSource;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(Instance);
    }

    private void OnEnable()
    {
        musicAudioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void InitializeVolume()
    {
        sfxVolume = PlayerPrefs.GetFloat(sfxVolumeKey, 0.8f);
        var musicVolume = PlayerPrefs.GetFloat(musicVolumeKey, 0.8f);
        var sliders = FindObjectsOfType<CustomSlider>(true);
        var sfxSlider = sliders.First(s => s.CompareTag("SFXSlider"));
        var musicSlider = sliders.First(s => s.CompareTag("MusicSlider"));

        if (sfxSlider != null)
        {
            sfxSlider.SetValue(sfxVolume);
            sfxSlider.onValueChange.AddListener(OnSfxVolumeChange);
        }
        if (musicSlider != null)
        {
            musicSlider.SetValue(musicVolume);
            musicSlider.onValueChange.AddListener(OnMusicVolumeChange);
        }
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        switch (scene.name)
        {
            case "Level":
                musicAudioSource.clip = Level.Instance.levelDetails.currentLevelTheme.backgroundMusic;
                musicAudioSource.Play();
                break;
            default:
                if (musicAudioSource.clip != GameManager.Instance.settings.introMusic)
                {
                    musicAudioSource.clip = GameManager.Instance.settings.introMusic;
                    musicAudioSource.Play();
                }
                break;
        }
        if (scene.name == "Level" || scene.name == "Main Menu")
        {
            InitializeVolume();
        }
    }

    public void OnSfxVolumeChange(float volume)
    {
        PlayerPrefs.SetFloat(sfxVolumeKey, volume);
        StaticEventHandler.CallOnSFXVolumeChange(sfxVolume, volume);
        sfxVolume = volume;
    }

    public void OnMusicVolumeChange(float volume)
    {
        musicAudioSource.volume = volume;
        PlayerPrefs.SetFloat(musicVolumeKey, volume);
    }

    public void PlaySound(SoundEffectSO sound)
    {
        var soundEffect = PoolManager.Instance.ReuseComponent<SoundEffect>(Vector3.zero, Quaternion.identity);
        soundEffect.SetSound(sound);
        soundEffect.gameObject.SetActive(true);
        StartCoroutine(StopSound(soundEffect, sound.clip.length));
    }

    IEnumerator StopSound(SoundEffect soundEffect, float length)
    {
        yield return new WaitForSeconds(length);
        soundEffect.gameObject.SetActive(false);
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {

    }
#endif
    #endregion
}