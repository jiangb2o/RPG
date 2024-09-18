using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AddObjectToScene("Player");
    }

    public void AddObjectToScene(string resourceName)
    {
        Object obj = Resources.Load(resourceName);
        Instantiate(obj);

        obj = null;
        Resources.UnloadUnusedAssets();
    }
}
