using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawnAI : SingletonMonoBehaviour<MonoBehaviour>
{
    public Vector3 enemySpawnPos;
    [HideInInspector] public bool spawnConsistent;
    [HideInInspector] public float maxMana;
    [HideInInspector] public float currentManaAmount;
    [HideInInspector] public float manaPerSecond;
    List<EnemyDetailsSO> enemyCastList = new();
    private readonly float regenerateManaTimeInterval = 0.25f;

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
        spawnConsistent = Level.Instance.levelDetails.enemySpawnConsistent;
        enemyCastList.AddRange(Level.Instance.levelDetails.enemySpawnableList);
        StartCoroutine(SpawnEnemies());
        StartCoroutine(RegenerateMana());
    }

    private IEnumerator RegenerateMana()
    {
        yield return new WaitForSeconds(regenerateManaTimeInterval);
        currentManaAmount += manaPerSecond * regenerateManaTimeInterval;
        if (currentManaAmount > maxMana)
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
        SpawnOption option = SpawnOption.Normal;
        while (!Level.Instance.isGameEnded)
        {
            if (option == SpawnOption.Slow)
            {
                yield return new WaitForSeconds(6);
            } else if (option == SpawnOption.Normal)
            {
                yield return new WaitForSeconds(4);
            } else
            {
                yield return new WaitForSeconds(2);
            }

            // Get random enemy
            EnemyDetailsSO enemyDetails = GetRandomEnemy();
            if (enemyDetails == null)
                spawnOption = SpawnOption.Slow;
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