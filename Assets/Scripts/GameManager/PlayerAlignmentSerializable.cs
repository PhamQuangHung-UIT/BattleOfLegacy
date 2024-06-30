using System.Collections.Generic;
using System.IO;
using System.Linq;

public class PlayerAlignmentSerializable : ISerializable
{
    public List<UnitBaseStatsSO> unitAlignment = GameManager.Instance.settings.beginUnitAlignment.ToList();

    public List<SpellBase> spellAlignment = GameManager.Instance.settings.beginSpellAlignment.ToList();

    public void LoadData(Stream stream)
    {
        using var reader = new BinaryReader(stream);
        int count = reader.ReadInt32();

        unitAlignment.Clear();
        for (int i = 0; i < count; i++)
        {
            string name = reader.ReadString();
            var unit = GameManager.Instance.settings.allObtainableUnit.First(u => u.unitName == name);
            unitAlignment.Add(unit);
        }

        count = reader.ReadInt32();

        spellAlignment.Clear();
        for (int i = 0; i < count; i++)
        {
            string name = reader.ReadString();
            var spell = GameManager.Instance.settings.allObtainableSpell.First(spell => spell.spellDetails.spellName == name);
            spellAlignment.Add(spell);

        }
    }

    public void SaveData(Stream stream)
    {
        using var writer = new BinaryWriter(stream);
        int count = unitAlignment.Count;
        writer.Write(count);
        for (int i = 0; i < count; i++)
        {
            writer.Write(unitAlignment[i].unitName);
        }
        count = spellAlignment.Count;
        writer.Write(count);
        for (int i = 0; i < count; i++)
        {
            writer.Write(spellAlignment[i].spellDetails.spellName);
        }
    }
}
