using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    float cooldownDuration = 1;
    int cost;
    bool isCooldownEnd = true;
    Button currentBtn;
    [SerializeField] Image cover, cooldownImage, icon;
    [SerializeField] TextMeshProUGUI costText;

    private void Awake()
    {
        currentBtn = GetComponent<Button>();
        currentBtn.onClick.AddListener(OnPress);
    }

    private void Update()
    {
        if (Level.Instance.currentMana < cost || !isCooldownEnd)
        {
            cover.color = GameManager.Instance.settings.disableColorForCover;
            currentBtn.enabled = false;
        }
        else
        {
            currentBtn.enabled = true;
            cover.color = new(0, 0, 0, 0f);
        }
    }

    public void OnPress()
    {
        isCooldownEnd = false;
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        float currentTime = 0;
        cooldownImage.fillAmount = 1;
        while (currentTime < cooldownDuration)
        {
            yield return null;
            currentTime += Time.deltaTime;
            cooldownImage.fillAmount = 1 - currentTime / cooldownDuration;
        }
        cooldownImage.fillAmount = 0;
        isCooldownEnd = true;
    }

    public void SetIcon(Sprite iconImage)
    {
        icon.sprite = iconImage;
    }

    public void SetCooldownDuration(float duration)
    {
        cooldownDuration = duration;
    }

    public void SetCost(int newCost)
    {
        cost = newCost;
        if (newCost > 1000000)
        {
            costText.text = newCost / 1000000 + "M";
        }
        else if (newCost > 1000)
        {
            costText.text = newCost / 1000 + "K";
        }
        else costText.text = newCost.ToString();
    }

    public void RemoveSlot()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
