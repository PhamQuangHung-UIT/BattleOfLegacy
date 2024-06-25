using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDetails_", menuName = "Scriptable Objects/Enemy/EnemyDetails")]
public class EnemyDetailsSO : ScriptableObject
{
    #region Tooltip
    [Tooltip("Amount of gold awarded when the enemy is dead")]
    #endregion
    public int goldAwarded;

    #region Tooltip
    [Tooltip("Level of the enemy")]
    #endregion
    public int level;

    #region Tooltip
    [Tooltip("Details of the enemy unit")]
    #endregion
    public UnitBaseStatsSO unitDetails;
}
