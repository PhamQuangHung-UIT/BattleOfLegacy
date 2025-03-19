public enum UnitState
{
    Idle,
    Run,
    Attack,
    Hurt,
    FirstSkill,
    SecondSkill,
    Dead
}

public enum TargetType
{
    Ally,
    Target,
    Both,
}

public enum RangeType
{
    Single,
    Multiple,
}

public enum ProjectileType
{
    SingleTarget,
    Penetrate,
}

public enum ValueType
{
    Percentage,
    Absolute
}

public enum HitpointEffectType
{
    Damage,
    Heal
}

public struct DamageLayer
{
    public const int Physics = 1;
    public const int Magic = 1 >> 1;
    public const int BaseAttack = 1 >> 2;
    public const int SpecialSkill = 1 >> 3;
    public const int Spell = 1 >> 4;
}

public enum Theme
{
    Plain,
    DeadForest,
    DarkCave
}

public enum GridViewType
{
    Unit, Spell, Other
}
