using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(AudioSource))]
public class ButtonWithSound : MonoBehaviour
{
    [SerializeField] SoundEffectSO buttonClickSFX;
    AudioSource source;
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        source = GetComponent<AudioSource>();
    }

    protected void OnEnable()
    {
        button.onClick.AddListener(PlaySFX);
    }

    protected void OnDisable()
    {
        button.onClick.RemoveListener(PlaySFX);
    }

    private void PlaySFX()
    {
        if (buttonClickSFX != null)
        {
            source.volume = buttonClickSFX.volume;
            source.PlayOneShot(buttonClickSFX.clip);
        }
    }
}
