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
            slots[i].GetComponent<Button>().onClick.AddListener(() => OnPress(i));
        }
    }

    protected abstract void OnPress(int i);
}