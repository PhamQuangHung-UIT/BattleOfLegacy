using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[DisallowMultipleComponent]
public class SoundEffect : MonoBehaviour
{
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (source != null)
        {
            source.Play();
        }
    }

    private void OnDisable()
    {
        if (source != null)
        {
            source.Stop();
        }
    }

    public void SetSound(SoundEffectSO sound)
    {
        source.clip = sound.clip;
        source.volume = sound.volume * SoundManager.Instance.sfxVolume;
        source.pitch = sound.pitch;
    }
}