using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabLayout : MonoBehaviour
{
    [SerializeField] GameObject[] tabButtons;

    public void SetOnTop(GameObject ui)
    {
        ui.transform.SetAsLastSibling();
        GameObject tabButton = ui.transform.Find("Tab").gameObject;
        foreach (var button in tabButtons)
        {
            if (button != tabButton)
            {
                button.GetComponent<Image>().color = GameConsts.disableColor;
                button.GetComponentInChildren<Image>().color = GameConsts.disableColor;
            }
            else
            {
                button.GetComponent<Image>().color = Color.white;
                button.GetComponentInChildren<Image>().color = Color.white;
            }
        }
    }
}
