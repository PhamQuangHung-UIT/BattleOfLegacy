using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance { get => instance; }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else Destroy(gameObject);
    }
}
