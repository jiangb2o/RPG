using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    public GameObject objectPrefab;
    private int poolSize;
    Transform parentTransform;

    private List<GameObject> pool;

    public ObjectPool(GameObject objectPrefab, int poolSize, Transform transform)
    {
        this.objectPrefab = objectPrefab;
        this.poolSize = poolSize;
        parentTransform = transform;
        pool = new List<GameObject>();

        for(int i = 0; i < poolSize; ++i)
        {
            GameObject obj = GameObject.Instantiate(objectPrefab, parentTransform);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject Get()
    {
        foreach(GameObject obj in pool)
        {
            if(!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // 没有可用的对象
        GameObject newGameObject = GameObject.Instantiate(objectPrefab);
        newGameObject.SetActive(true);
        pool.Add(newGameObject);
        return newGameObject;
    }
    
    public void Return(GameObject obj)
    {
        obj.transform.parent = parentTransform;
        obj.SetActive(false);
    }
}
