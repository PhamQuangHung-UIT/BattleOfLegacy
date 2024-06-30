using UnityEngine;
using UnityEngine.UI;

public abstract class SlotPanel : MonoBehaviour
{
    protected Slot[] slots;

    protected virtual void Awake()
    {
        slots = GetComponentsInChildren<Slot>();
        for (int i = 0; i < slots.Length; i++)
        {
            int index = i;
            slots[i].GetComponent<Button>().onClick.AddListener(() => OnPress(index));
        }
    }

    // Call when a slot item is selected
    protected abstract void OnPress(int i);
}