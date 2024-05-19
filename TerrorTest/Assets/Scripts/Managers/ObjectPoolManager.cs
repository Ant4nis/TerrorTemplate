using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    [Header("Configuration")] 
    [Tooltip("Where do you want to create the pool")] [SerializeField]
    private GameObject poolContainer;
    
    private Dictionary<string, Queue<GameObject>> poolDictionary;
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<Pool> pools;

    private void Awake()
    {
        Instance = this;
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectQueue = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, poolContainer.transform);
                obj.SetActive(false);
                objectQueue.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectQueue);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
            if (!poolDictionary.ContainsKey(tag) || poolDictionary[tag].Count == 0)
            {
                Debug.LogWarning("No objects available in the pool for tag: " + tag);
                return null;
            }

            GameObject objectToSpawn = poolDictionary[tag].Dequeue();

            // We configure position and rotation before the object is activated.
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            objectToSpawn.SetActive(true);

            poolDictionary[tag].Enqueue(objectToSpawn);

            return objectToSpawn;
    }
}