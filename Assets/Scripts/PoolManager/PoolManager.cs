using System;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PoolManager : SingletonMonoBehaviour<PoolManager>
{
    #region Tooltip
    [Tooltip("Populate this array with prefabs that you want to add to the pool, and specify the number of gameobjects to be created for each.")]
    #endregion
    [SerializeField] private Pool[] poolArray = null;
    private Transform objectPoolTransform;
    private readonly Dictionary<string, Queue<Component>> poolDictionary = new();
    private readonly Dictionary<string, GameObject> prefabDictionary = new();
    public readonly Dictionary<string, GameObject> anchorDictionary = new();

    [System.Serializable]
    public struct Pool
    {
        public GameObject prefab;
        public string componentType;
        public int poolSize;
    }

    protected override void Awake()
    {
        base.Awake();
        // This singleton gameobject will be the object pool parent
        objectPoolTransform = gameObject.transform;

        // Create object pools on start
        for (int i = 0; i < poolArray.Length; i++)
        {
            CreatePool(poolArray[i].prefab, poolArray[i].poolSize, poolArray[i].componentType);
        }

    }

    /// <summary>
    /// Create the object pool with the specified prefabs and the specified pool size for each
    /// </summary>
    private void CreatePool(GameObject prefab, int poolSize, string componentType)
    {
        prefabDictionary.Add(componentType, prefab);

        string prefabName = prefab.name; // get prefab name

        GameObject parentGameObject = new(prefabName + "Anchor"); // create parent gameobject to parent the child objects to

        anchorDictionary.Add(componentType, parentGameObject);

        parentGameObject.transform.SetParent(objectPoolTransform);

        if (!poolDictionary.ContainsKey(componentType))
        {
            poolDictionary.Add(componentType, new Queue<Component>());

            for (int i = 0; i < poolSize; i++)
            {
                GameObject newObject = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation, parentGameObject.transform);

                newObject.SetActive(false);

                poolDictionary[componentType].Enqueue(newObject.GetComponent(Type.GetType(componentType)));

            }
        }

    }

    /// <summary>
    /// Reuse a gameobject component in the pool.  'prefab' is the prefab gameobject containing the component. 'position' is the world position for the gameobject where it should appear when enabled. 'rotation' should be set if the gameobject needs to be rotated.
    /// </summary>
    public T ReuseComponent<T>(Vector3 position, Quaternion rotation) where T : Component
    {
        string componentType = typeof(T).Name;
        if (poolDictionary.ContainsKey(componentType))
        {
            // Get object from pool queue
            T componentToReuse = GetComponentFromPool<T>(componentType);

            ResetObject(position, rotation, componentToReuse);

            return componentToReuse;
        }
        else
        {
            Debug.Log("No object pool for " + componentType);
            return null;
        }
    }

    /// <summary>
    /// Get a gameobject component from the pool using the 'poolKey'
    /// </summary>
    private T GetComponentFromPool<T>(string componentType) where T: Component
    {
        T componentToReuse = poolDictionary[componentType].Dequeue() as T;
        poolDictionary[componentType].Enqueue(componentToReuse);

        if (componentToReuse.gameObject.activeSelf == true)
        {
            GameObject newObject = Instantiate(prefabDictionary[componentType], anchorDictionary[componentType].transform);

            newObject.SetActive(false);

            poolDictionary[componentType].Enqueue(newObject.GetComponent(Type.GetType(componentType)));
        }

        return componentToReuse;
    }

    /// <summary>
    /// Reset the gameobject.
    /// </summary>
    private void ResetObject(Vector3 position, Quaternion rotaion, Component componentToReuse)
    {
        componentToReuse.transform.position = position;
        componentToReuse.transform.rotation = rotaion;
        componentToReuse.gameObject.transform.localScale = Vector3.one;
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        foreach (var e in poolArray)
        {
            ValidateUtilities.AssertException(() =>
            {
                Type.GetType(e.componentType);
            }, $"The type '{e.componentType}' as object pool type is invalid");
        }
    }
#endif
    #endregion
}
