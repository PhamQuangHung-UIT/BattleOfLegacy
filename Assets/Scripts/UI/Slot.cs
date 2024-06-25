using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public bool canUsed = true;
    float timeInterval = 1;
    float cost;
    Button currentBtn;
    [SerializeField] Image slotImage, cooldownImage, icon;
    [SerializeField] TextMeshProUGUI costText;

    private void Awake()
    {
        currentBtn = GetComponent<Button>();
    }

    private void Start()
    {
        if (!canUsed)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (Level.Instance.currentMana > cost)
        {
            slotImage.color = new(0, 0, 0, 0.7f);
        }
        else slotImage.color = new(0, 0, 0, 0f);
    }

    public void OnPress()
    {
        currentBtn.enabled = false;
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        float currentTime = 0;
        cooldownImage.fillAmount = 1;
        while (currentTime < timeInterval)
        {
            yield return null;
            currentTime += Time.deltaTime;
            cooldownImage.fillAmount = 1 - currentTime / timeInterval;
        }
        cooldownImage.fillAmount = 0;
        currentBtn.enabled = true;
    }

    public void SetIcon(Sprite iconImage)
    {
        icon.sprite = iconImage;
    }

    public void SetCost(int newCost)
    {
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
}
