using System.Collections;
using TMPro;
using UnityEngine;

public class GoldDrop : MonoBehaviour
{
    public float speed;
    public float delay;
    public AnimationCurve curve;
    public int amount;

    private void OnEnable()
    {
        StartCoroutine(GoldDropEffect());
        GetComponentInChildren<TextMeshProUGUI>().text = amount.ToString();
    }

    IEnumerator GoldDropEffect()
    {
        float time = 0;
        float m_speed = speed;
        while (time < delay)
        {
            transform.Translate(m_speed * Time.deltaTime * Vector2.up);
            yield return null;
            time += Time.deltaTime;
            m_speed = speed * curve.Evaluate(time / delay);
        }
        gameObject.SetActive(false);
    }
}