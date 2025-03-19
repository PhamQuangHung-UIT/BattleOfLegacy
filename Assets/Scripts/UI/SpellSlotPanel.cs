using System.Collections.Generic;

public class SpellSlotPanel : SlotPanel
{
    List<SpellBase> spells;
    protected override void Awake()
    {
        base.Awake();
        spells = GameManager.Instance.playerData.playerAlignment.spellAlignment;
        for (int i = 0; i < slots.Length; i++)
        {
            Slot slot = slots[i];
            if (i < spells.Count)
            {
                slot.SetIcon(spells[i].spellDetails.image);
                slot.SetCooldownDuration(spells[i].spellDetails.spellCooldown);
                slot.SetCost((int) spells[i].spellDetails.manaCost);
            }
            else slot.RemoveSlot();
        }
    }

    protected override void OnPress(int i)
    {
        Level.Instance.currentMana -= spells[i].spellDetails.manaCost;
        SpellBase spell = Instantiate(spells[i]);
        Level.Instance.ExecuteSpell(spell);
    }
}