using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level : SingletonMonoBehaviour<Level>
{
    public Vector2 playerSpawnPos, enemySpawnPos;

    [Header("User Interface")]
    public TextMeshProUGUI currentManaText, manaPerSecondText, goldGainedText;

    [HideInInspector] public LevelDetailsSO levelDetails;

    [HideInInspector] 
    public List<Unit> unitList = new();

    [Header("Enviroment")]
    public GameObject playerStatue, enemyStatue;
    [Header("UI")]
    public VictoryPanel victoryPanel;
    public DefeatPanel defeatPanel;

    [HideInInspector] public int goldGained = 0;

    [HideInInspector] public float goldGainedRate = 1;

    [HideInInspector] public float maxMana, currentMana;

    [HideInInspector] public float manaPerSecond;

    [HideInInspector] public bool isGameEnded;

    private void Start()
    {
        StaticEventHandler.OnReceiveGold += OnReceiveGold;
        StaticEventHandler.OnVictory += OnVictory;
        StaticEventHandler.OnDefeat += OnDefeat;
        GetLevelDetail();
        manaPerSecondText.text = manaPerSecond + " mana/s";
    }
 
    private void Update()
    {
        if (isGameEnded) return;

        currentMana += manaPerSecond * Time.deltaTime;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
        currentManaText.text = (int)currentMana + "/" + (int)maxMana;
        goldGainedText.text = goldGained.ToString();
    }

    private void OnDisable()
    {
        StaticEventHandler.OnReceiveGold -= OnReceiveGold;
        StaticEventHandler.OnVictory -= OnVictory;
        StaticEventHandler.OnDefeat -= OnDefeat;
    }

    private void GetLevelDetail()
    {
        levelDetails = LevelManager.Instance.levelDetails[LevelManager.Instance.currentLevelIndex];

        UpgradeManager.Instance.data.statueAttributeLevels.TryGetValue("goldGainRate", out int goldGainRateLevel);
        goldGainedRate = UpgradeManager.Instance.statueUpgradeDetails.goldGainRatePerLevelDetails[goldGainRateLevel].amount;

        UpgradeManager.Instance.data.statueAttributeLevels.TryGetValue("maxMana", out int maxManaLevel);
        maxMana = UpgradeManager.Instance.statueUpgradeDetails.maxManaPerLevelDetails[maxManaLevel].amount;

        UpgradeManager.Instance.data.statueAttributeLevels.TryGetValue("manaRecoverSpeed", out int manaPerSecondLevel);
        manaPerSecond = UpgradeManager.Instance.statueUpgradeDetails.manaRecoverSpeedPerLevelDetails[manaPerSecondLevel].amount;
    }

    public bool SpawnUnit(UnitBaseStatsSO unitDetails, EnemyDetailsSO enemyDetails = null)
    {
        bool isEnemy = enemyDetails != null;
        Vector3 spawnPos = isEnemy ? enemySpawnPos : playerSpawnPos;
        Unit unit = PoolManager.Instance.ReuseComponent<Unit>( 
                                spawnPos, Quaternion.identity);
        int unitLevel;
        if (!isEnemy)
            unitLevel = UpgradeManager.Instance.data.unitLevels[unitDetails.unitName];
        else unitLevel = enemyDetails.level - 1;
        unit.Initialized(unitDetails, unitLevel, isEnemy);

        unit.gameObject.SetActive(true);

        // Set up enemy behaviour
        Enemy enemy = unit.GetComponent<Enemy>();
        if (isEnemy)
        {
            enemy.enemyDetails = enemyDetails;
        }
        enemy.enabled = isEnemy;

        unitList.Add(unit);
        return true;
    }


    private void OnReceiveGold(OnReceiveGoldEventArgs args)
    {
        goldGained += (int)(args.amount * goldGainedRate);
    }

    private void OnDefeat(EventArgs args)
    {
        isGameEnded = true;
    }

    private void OnVictory(EventArgs args)
    {
        isGameEnded = true;
    }

    public GameObject GetStatue(bool isEnemy)
    {
        return isEnemy ? enemyStatue : playerStatue;
    }

    public void ExecuteSpell(SpellBase spell)
    {
        spell.Initialized(UpgradeManager.Instance.data.spellLevels[spell.spellDetails.spellName]);
        spell.Execute();
    }
}
