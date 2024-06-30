using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class VictoryPanel : MonoBehaviour
{
    public TextMeshProUGUI enemyGoldGainValue, rewardGoldValueUI, rewardGemValueUI;
    public GameObject rewardGemUI;
    public SoundEffectSO victorySound;
    int enemyGoldGained;
    int rewardGold;
    int rewardGem;

    public void OnEnable()
    {
        SetUp();
        GameManager.Instance.playerData.currentGold += rewardGold + enemyGoldGained;
        GameManager.Instance.playerData.currentGem += rewardGem;
        GameManager.Instance.SaveData();
        LevelManager.Instance.CompleteLevel();
        SoundManager.Instance.PlaySound(victorySound);
    }

    public void SetUp()
    {
        enemyGoldGained = Level.Instance.goldGained;
        bool firstLevelPlay = StagePanel.selectedLevel == StagePanel.currentLevel;
        rewardGold = firstLevelPlay ? Level.Instance.levelDetails.firstTimeRewardedGold : Level.Instance.levelDetails.replayRewardedGold;
        rewardGem = firstLevelPlay ? Level.Instance.levelDetails.rewardedGems : 0;
        enemyGoldGainValue.text = enemyGoldGained.ToString();
        rewardGoldValueUI.text = rewardGold.ToString();
        if (firstLevelPlay)
        {
            rewardGemValueUI.text = rewardGem.ToString();
        }
        rewardGemUI.SetActive(firstLevelPlay);
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