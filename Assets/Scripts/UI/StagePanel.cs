using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StagePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI stageText;
    [SerializeField] Button nextButton, prevButton, playButton;
    [SerializeField] TextMeshProUGUI rewardValue;
    [SerializeField] TextMeshProUGUI firstTimeRewardGoldValue, firstTimeRewardGemValue;
    [SerializeField] GameObject firstTimeRewardParent;
    public static int selectedLevel, currentLevel;
    private void Awake()
    {
        selectedLevel = LevelManager.Instance.data.selectedLevel;
        currentLevel = LevelManager.Instance.data.currentLevel;
        nextButton.onClick.AddListener(Next);
        prevButton.onClick.AddListener(Previous);
        playButton.onClick.AddListener(Play);
        OnSelectLevel(selectedLevel);
    }

    public void Next()
    {
        OnSelectLevel(++selectedLevel);
    }

    public void Previous()
    {
        OnSelectLevel(--selectedLevel);
    }

    private void Play()
    {
        LevelManager.Instance.currentLevelIndex = selectedLevel;
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            Level.Instance.levelDetails = LevelManager.Instance.levelDetails[selectedLevel];
        };
        GameManager.Instance.GoToLevel();
    }

    void OnSelectLevel(int level)
    {
        var levelDetails = LevelManager.Instance.levelDetails[level];
        prevButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);
        if (level == 0)
        {
            prevButton.gameObject.SetActive(false);
        }
        if (level == currentLevel)
        {
            firstTimeRewardGoldValue.text = levelDetails.firstTimeRewardedGold.ToString();
            firstTimeRewardGemValue.text = levelDetails.rewardedGems.ToString();
            nextButton.gameObject.SetActive(false);
        }
        rewardValue.text = levelDetails.replayRewardedGold.ToString();
        firstTimeRewardParent.SetActive(level == currentLevel);
        stageText.text = $"Stage {selectedLevel + 1}";
    }
}
