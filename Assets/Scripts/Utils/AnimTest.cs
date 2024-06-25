using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour
{
    Animator anim;
    public int[] values;

    private void Start()
    {
        anim = GetComponent<Animator>();
        Debug.Log(Application.persistentDataPath);
    }

    private void Update()
    {
        var list = anim.GetCurrentAnimatorClipInfo(0);
        foreach (var clipInfo in list)
        {
            Debug.Log($"Count: {list.Length}");
            Debug.Log(clipInfo.clip);
        }
    }
}