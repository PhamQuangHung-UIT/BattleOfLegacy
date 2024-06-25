using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : SingletonMonoBehaviour<UpgradeManager>
{
    public const string fileName = "upgrades.dat";

    public StatueAttributeUpgradeSO statueUpgradeDetails;

    public UpgradeSerializableData data;

    private void Start()
    {
        data = new();
        StorageUtils.Load(data, fileName);
    }

    public void Upgrade(string unitName, int level)
    {
        if (!data.unitLevels.TryAdd(unitName, level))
        {
            data.unitLevels[unitName] = level;
        }
        StorageUtils.Save(data, fileName);
    }
}