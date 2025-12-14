using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    [System.Serializable]
    public class Pool
    {
        public string tag;              
        public string addressKey;      
        public int size;               
        public Transform parent;        
    }

    [Header("Pools")]
    public List<Pool> pools;

    private Dictionary<string, Queue<GameObject>> poolDictionary =
        new Dictionary<string, Queue<GameObject>>();

    public bool IsReady { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    IEnumerator Start()
    {
        yield return InitializePools();
    }

    // -------------------- INITIALIZATION --------------------

    IEnumerator InitializePools()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                AsyncOperationHandle<GameObject> handle =
                    Addressables.InstantiateAsync(pool.addressKey);

                yield return handle;

                if (handle.Status != AsyncOperationStatus.Succeeded)
                {
                    Debug.LogError($"Failed to load Addressable: {pool.addressKey}");
                    continue;
                }

                GameObject obj = handle.Result;
                obj.SetActive(false);

                if (pool.parent != null)
                    obj.transform.SetParent(pool.parent);

                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }

        IsReady = true;
        Debug.Log("ObjectPooler: All pools initialized.");
    }

    // -------------------- SPAWN --------------------

    public GameObject Spawn(string tag, Vector3 position, Quaternion rotation)
    {
        if (!IsReady)
        {
            Debug.LogWarning("ObjectPooler not ready yet.");
            return null;
        }

        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogError("Pool with tag '" + tag + "' does not exist.");
            return null;
        }

        Queue<GameObject> pool = poolDictionary[tag];

        if (pool.Count == 0)
        {
            Debug.LogWarning("Pool empty for tag: " + tag);
            return null;
        }

        GameObject obj = pool.Dequeue();

        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);

        return obj;
    }

    // -------------------- RETURN --------------------

    public void ReturnToPool(string tag, GameObject obj)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogError("Trying to return to invalid pool: " + tag);
            Destroy(obj);
            return;
        }

        obj.SetActive(false);
        poolDictionary[tag].Enqueue(obj);
    }
}
