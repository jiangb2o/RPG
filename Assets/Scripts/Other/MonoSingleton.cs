using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static bool appQuitting = false;
    public static T Instance
    {
        get
        {
            // Destroy 后在当前帧仍可以使用
            if (appQuitting)
                return instance;
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name + " Singleton");
                    instance = obj.AddComponent<T>();
                    if (obj.transform.root == obj.transform)
                    {
                        DontDestroyOnLoad(obj);
                    }
                }
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnApplicationQuit()
    {
        appQuitting = true;
    }
}
