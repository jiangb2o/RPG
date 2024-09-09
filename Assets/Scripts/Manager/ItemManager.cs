using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Scripting;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }
    public ItemDatabase itemDB;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    

    public ItemScriptObject GetRandomItem()
    {
        int index = Random.Range(0, itemDB.itemList.Count);
        return itemDB.itemList[index];
    }

}
