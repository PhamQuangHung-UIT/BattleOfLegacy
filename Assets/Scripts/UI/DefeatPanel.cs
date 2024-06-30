using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class DefeatPanel : MonoBehaviour
{
    public TextMeshProUGUI enemyGoldGainValue;
    public SoundEffectSO defeatSound;
    int enemyGoldGained;

    public void OnEnable()
    {
        SetUp();
        if (defeatSound != null)
            SoundManager.Instance.PlaySound(defeatSound);
        GameManager.Instance.playerData.currentGold += enemyGoldGained;
        GameManager.Instance.SaveData();
    }

    public void SetUp()
    {
        enemyGoldGained = Level.Instance.goldGained;
        enemyGoldGainValue.text = enemyGoldGained.ToString();
    }

    public void Continue()
    {
        GameManager.Instance.GoToSelectLevel();
    }

    public void Replay()
    {
        GameManager.Instance.GoToLevel();
    }

}