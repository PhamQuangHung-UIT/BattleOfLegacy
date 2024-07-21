using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    Image mask;
    public Image healthBar;
    [HideInInspector] public float value;
    [HideInInspector] public float originWidth;

    void Awake()
    {
        mask = GetComponentInChildren<Mask>().gameObject.GetComponent<Image>();
        originWidth = mask.rectTransform.rect.width;
    
    }

    public void Initialized(bool isEnemy)
    {
        healthBar.color = isEnemy ? GameConsts.enemyHealthbarColor : GameConsts.playerHealthbarColor;
    }

    public void SetValue(float value)
    {
        this.value = value;
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originWidth * value);
    }

    #region Unity Editor
#if UNITY_EDITOR
    private void OnValidate()
    {
        ValidateUtilities.AssertNotNull(GetComponentInChildren<Mask>().gameObject.GetComponent<Image>(), "Image (HealthBar)");
    }
#endif
    #endregion
}
