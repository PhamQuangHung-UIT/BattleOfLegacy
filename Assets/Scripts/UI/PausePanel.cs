using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1.0f;
    }

    public void OnSfxVolumeChange(float volume)
    {
        SoundManager.Instance.OnSfxVolumeChange(volume);
    }

    public void OnMusicVolumeChange(float volume)
    {
        SoundManager.Instance.OnMusicVolumeChange(volume);
    }

    public void ReturnHome()
    {
        GameManager.Instance.GoToMainMenu();
    }
}
