using System.Collections.Generic;
using UnityEngine;

// This script handles object pooling for any prefab
public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 10;

    private List<GameObject> pool;

    void Awake()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    // Get an inactive object from the pool
    public GameObject GetObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // Optional: Expand the pool if none are available
        GameObject newObj = Instantiate(prefab);
        newObj.SetActive(false);
        pool.Add(newObj);

        newObj.SetActive(true);
        return newObj;
    }

    // Return the object back to the pool
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
