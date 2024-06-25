using System;
using System.Collections;
using System.Collections.Generic;
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
    int selectedLevel, currentLevel;
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
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            Level.Instance.levelDetails = LevelManager.Instance.levelDetails[selectedLevel - 1];
        };
        GameManager.Instance.GoToLevel();
    }

    void OnSelectLevel(int level)
    {
        var levelDetails = LevelManager.Instance.levelDetails[level - 1];
        if (level == 1)
        {
            prevButton.gameObject.SetActive(false);
        } else if (level == currentLevel)
        {
            firstTimeRewardGoldValue.text = levelDetails.firstTimeRewardedGold.ToString();
            firstTimeRewardGemValue.text = levelDetails.rewardedGems.ToString();
            nextButton.gameObject.SetActive(false);
        } else
        {
            prevButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(true);
        }
        rewardValue.text = levelDetails.replayRewardedGold.ToString();
        firstTimeRewardGemValue.gameObject.SetActive(level == currentLevel);
        firstTimeRewardGoldValue.gameObject.SetActive(level == currentLevel);
        stageText.text = $"Stage {selectedLevel}";
    }
}
