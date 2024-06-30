using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabLayout : MonoBehaviour
{
    [System.Serializable]
    public class Tab
    {
        public Button tabButton;
        public GameObject content;
    }

    [SerializeField] Tab[] tabButtons;

    private void Start()
    {
        foreach (Tab tab in tabButtons)
        {
            tab.tabButton.onClick.AddListener(() => SetOnTop(tab));
        }
        tabButtons[0].tabButton.onClick.Invoke();
    }

    public void SetOnTop(Tab currentTab)
    {
        currentTab.content.transform.SetAsLastSibling();
        currentTab.tabButton.transform.SetAsLastSibling();
        foreach (var tab in tabButtons)
        {
            if (tab != currentTab)
            {
                tab.tabButton.GetComponent<Image>().color = GameManager.Instance.settings.disableColor;
                tab.tabButton.transform.GetChild(0).GetComponent<Image>().color = GameManager.Instance.settings.disableColor;
            }
            else
            {
                tab.tabButton.GetComponent<Image>().color = Color.white;
                tab.tabButton.transform.GetChild(0).GetComponent<Image>().color = Color.white;
            }
        }
    }
}
