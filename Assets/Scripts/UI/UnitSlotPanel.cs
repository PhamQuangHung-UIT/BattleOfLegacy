using System.Collections.Generic;

public class UnitSlotPanel : SlotPanel
{
    List<UnitBaseStatsSO> units;

    protected override void Awake()
    {
        base.Awake();
        units = GameManager.Instance.playerData.playerAlignment.unitAlignment;
        for (int i = 0; i < slots.Length; i++)
        {
            Slot slot = slots[i];
            if (i < units.Count)
            {
                slot.SetIcon(units[i].image);
                slot.SetCooldownDuration(units[i].cooldownInterval);
                slot.SetCost((int) units[i].manaCost);
            }
            else slot.RemoveSlot();
        }
    }

    protected override void OnPress(int i)
    {
        if (!Level.Instance.isGameEnded)
        {
            Level.Instance.SpawnUnit(units[i]);
            Level.Instance.currentMana -= units[i].manaCost;
        }
    }
}