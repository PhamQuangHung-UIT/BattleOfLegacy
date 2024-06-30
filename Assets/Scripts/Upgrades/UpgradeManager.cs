using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : SingletonMonoBehaviour<UpgradeManager>
{
    public const string fileName = "upgrades.dat";

    public StatueAttributeUpgradeSO statueUpgradeDetails;

    public UpgradeSerializableData data;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        statueUpgradeDetails.Init();
        data = new();
        StorageUtils.Load(data, fileName);
    }

    public void UpgradeUnit(string unitName, int level)
    {
        if (!data.unitLevels.TryAdd(unitName, level))
        {
            data.unitLevels[unitName] = level;
        }
        StorageUtils.Save(data, fileName);
    }

    public void UpgradeSpell(string spellName, int level)
    {
        if (!data.spellLevels.TryAdd(spellName, level))
        {
            data.spellLevels[spellName] = level;
        }
        StorageUtils.Save(data, fileName);
    }

    public void UpgradeStatueAttribute(string attributeName, int level)
    {
        if (!data.statueAttributeLevels.TryAdd(attributeName, level))
        {
            data.statueAttributeLevels[attributeName] = level;
        }
        StorageUtils.Save(data, fileName);
    }
}