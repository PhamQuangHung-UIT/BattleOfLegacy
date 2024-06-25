using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConsts
{
    /// <summary>
    /// Minimum attack speed
    /// </summary>
    public const float minAttackSpeed = 0.1f;

    /// <summary>
    /// Maximum attack speed
    /// </summary>
    public const float maxAttackSpeed = 10f;

    /// <summary>
    /// Max level that a unit can get
    /// </summary>
    public const int maxUnitLevel = 10;

    public const float parallaxMaxPoint = 10;

    /// <summary>
    /// Maximum distance between two object to be considered "collided"
    /// </summary>
    public const float collideEpsillon = 0.2f;

    /// <summary>
    /// Max range of projectile to travel
    /// </summary>
    public const float maxProjectileRange = 255f;

    public static readonly Color playerHealthbarColor = Color.green;

    public static readonly Color enemyHealthbarColor = Color.red;
    public static readonly Color disableColor = new(0.3f, 0.3f, 0.3f);
}
