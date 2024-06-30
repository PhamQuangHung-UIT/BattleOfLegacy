using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[DisallowMultipleComponent]
public class SoundEffect : MonoBehaviour
{
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        StaticEventHandler.OnSFXVolumeChange += StaticEventHandler_OnSFXVolumeChange;
        if (source != null)
        {
            source.Play();
        }
    }

    private void OnDisable()
    {
        StaticEventHandler.OnSFXVolumeChange -= StaticEventHandler_OnSFXVolumeChange;
        if (source != null)
        {
            source.Stop();
        }
    }

    private void StaticEventHandler_OnSFXVolumeChange(OnVolumeChangeArgs args)
    {
        source.volume *= args.newVolume / args.oldVolume;
    }

    public void SetSound(SoundEffectSO sound)
    {
        source.clip = sound.clip;
        source.volume = sound.volume * SoundManager.Instance.sfxVolume;
        source.pitch = sound.pitch;
    }
}