using UnityEngine;

[CreateAssetMenu(fileName = "Level_", menuName = "Scriptable Objects/Level/LevelDetails")]
public class LevelDetailsSO : ScriptableObject
{
    private static int m_id = 1;
    #region Tooltip
    [Tooltip("Current level number")]
    #endregion
    public int id = m_id;

    #region Tooltip
    [Tooltip("Rewarded gold when complete the level in first time")]
    #endregion
    public int firstTimeRewardedGold;

    #region Tooltip
    [Tooltip("Rewarded gold when complete the level in later time")]
    #endregion
    public int replayRewardedGold;

    #region Tooltip
    [Tooltip("Rewarded gems of the level")]
    #endregion
    public int rewardedGems;

    public LevelThemeSO currentLevelTheme;

    public bool enemySpawnConsistent = false;

    public EnemyDetailsSO[] enemySpawnableList;

    public float enemyStatueMaxHealth;

    public float enemyMaxMana;

    public float enemyManaPerSecond;

#if UNITY_EDITOR
    private void Reset()
    {
        ++m_id;
    }

    private void OnValidate()
    {
        ValidateUtilities.AssertEmptyList(enemySpawnableList);
    }
#endif
}
