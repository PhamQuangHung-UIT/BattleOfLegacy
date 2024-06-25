using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    private const string sfxVolumeKey = "sfxVolume";
    private const string musicVolumeKey = "musicVolume";

    AudioSource musicAudioSource;

    [HideInInspector] public float sfxVolume, musicVolume;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        musicAudioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void Start()
    {
        InitializeVolume();
    }

    private void InitializeVolume()
    {
        sfxVolume = PlayerPrefs.GetFloat(sfxVolumeKey, 1);
        musicVolume = PlayerPrefs.GetFloat(musicVolumeKey, 1);
        var sliders = FindObjectsOfType<CustomSlider>(true);
        var sfxSlider = sliders.First(s => s.CompareTag("SFXSlider"));
        var musicSlider = sliders.First(s => s.CompareTag("MusicSlider"));

        if (sfxSlider != null)
        {
            sfxSlider.SetValue(sfxVolume);
        }
        if (musicSlider != null)
        {
            musicSlider.SetValue(musicVolume);
        }
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        switch (scene.name)
        {
            case "Main Menu":
            case "Select Level":
                if (musicAudioSource.clip != GameManager.Instance.settings.introMusic)
                {
                    musicAudioSource.clip = GameManager.Instance.settings.introMusic;
                    musicAudioSource.Play();
                }
                break;
            default:
                musicAudioSource.clip = Level.Instance.levelDetails.currentLevelTheme.backgroundMusic;
                break;
        }
        
    }

    public void OnSfxVolumeChange(float volume)
    {
        sfxVolume = volume;
        var objects = FindObjectsOfType<SoundEffect>();
        foreach (SoundEffect obj in objects)
        {
            obj.GetComponent<AudioSource>().volume = sfxVolume;
        }
        PlayerPrefs.SetFloat(sfxVolumeKey, sfxVolume);
    }

    public void OnMusicVolumeChange(float volume)
    {
        musicVolume = volume;
        musicAudioSource.volume = musicVolume;
        PlayerPrefs.SetFloat(musicVolumeKey, musicVolume);
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