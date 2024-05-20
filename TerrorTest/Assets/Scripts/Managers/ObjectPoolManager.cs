using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a pool of reusable objects to optimize performance by reusing inactive objects instead of creating and destroying them repeatedly.
/// </summary>
public class ObjectPoolManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the ObjectPoolManager.
    /// </summary>
    public static ObjectPoolManager Instance;

    [Header("Configuration")]
    [Tooltip("Where do you want to create the pool")]
    [SerializeField]
    private GameObject poolContainer;
    
    /// <summary>
    /// Dictionary to store pools of objects by their tag.
    /// </summary>
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    /// <summary>
    /// Represents a pool of objects with a specific tag, prefab, and size.
    /// </summary>
    [System.Serializable]
    public class Pool
    {
        [Tooltip("The tag to identify the pool.")]
        public string tag;

        [Tooltip("The prefab to instantiate objects from.")]
        public GameObject prefab;

        [Tooltip("The size of the pool.")]
        public int size;
    }

    /// <summary>
    /// List of pools to be created at the start.
    /// </summary>
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

    /// <summary>
    /// Spawns an object from the specified pool, setting its position and rotation.
    /// </summary>
    /// <param name="tag">The tag identifying the pool to spawn from.</param>
    /// <param name="position">The position to place the spawned object.</param>
    /// <param name="rotation">The rotation to set for the spawned object.</param>
    /// <returns>The spawned GameObject, or null if the pool is empty or does not exist.</returns>
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
