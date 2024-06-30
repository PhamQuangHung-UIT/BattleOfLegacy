using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawnAI : SingletonMonoBehaviour<MonoBehaviour>
{
    [HideInInspector] public bool spawnConsistent;
    [HideInInspector] public float maxMana;
    [HideInInspector] public float currentManaAmount;
    [HideInInspector] public float manaRecoverSpeed;
    List<EnemyDetailsSO> enemyCastList = new();
    private readonly float regenerateManaTimeInterval = 0.2f;

    enum SpawnOption
    {
        Slow,
        Normal,
        Fast
    }

    SpawnOption spawnOption = SpawnOption.Normal;
    float time;

    private void Start()
    {
        currentManaAmount = Level.Instance.levelDetails.enemyMaxMana;
        maxMana = currentManaAmount;
        manaRecoverSpeed = Level.Instance.levelDetails.enemyManaPerSecond;
        spawnConsistent = Level.Instance.levelDetails.enemySpawnConsistent;
        enemyCastList.AddRange(Level.Instance.levelDetails.enemySpawnableList);
        StartCoroutine(SpawnEnemies());
        StartCoroutine(RegenerateMana());
    }

    private IEnumerator RegenerateMana()
    {
        yield return new WaitForSeconds(regenerateManaTimeInterval);
        currentManaAmount += manaRecoverSpeed * regenerateManaTimeInterval;
        if (currentManaAmount > maxMana && spawnOption == SpawnOption.Slow)
        {
            spawnOption = SpawnOption.Fast;
        }
    }

    private void Update()
    {
        if (spawnOption == SpawnOption.Normal && !spawnConsistent)
        {
            time += Time.deltaTime;
            if (time > 60)
            {
                time = 0;
                spawnOption = SpawnOption.Fast;
            }
        }
    }

    private IEnumerator SpawnEnemies()
    {
        SpawnEnemy();
        while (!Level.Instance.isGameEnded)
        {
            if (spawnOption == SpawnOption.Slow)
            {
                yield return new WaitForSeconds(5);
            }
            else if (spawnOption == SpawnOption.Normal)
            {
                yield return new WaitForSeconds(3.5f);
            }
            else
            {
                yield return new WaitForSeconds(1.5f);
            }

            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        // Get random enemy
        EnemyDetailsSO enemyDetails = GetRandomEnemy();
        if (enemyDetails == null)
            spawnOption = SpawnOption.Slow;
        else
        {
            // Spawn enemy
            Level.Instance.SpawnUnit(enemyDetails.unitDetails, enemyDetails);
        }
    }

    private EnemyDetailsSO GetRandomEnemy()
    {
        List<EnemyDetailsSO> availableUnit = enemyCastList.Where(u => u.unitDetails.manaCost < currentManaAmount).ToList();
        if (availableUnit.Count == 0)
            return null;
        int index = Random.Range(0, availableUnit.Count);
        currentManaAmount -= availableUnit[index].unitDetails.manaCost;
        return availableUnit[index];
    }
}