using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoSingleton<InventoryManager>
{
    public List<ItemScriptObject> itemList;

    private void Start()
    {
        // add default weapon
        // AddItem(itemList[0]);
    }

    public void AddItem(ItemScriptObject itemSO)
    {
        itemList.Add(itemSO);
        InventoryUI.Instance.AddItem(itemSO);
        MessageUI.Instance.Show("获得了" + itemSO.itemName);
    }

    public void RemoveItem(ItemScriptObject itemSO)
    {
        itemList.Remove(itemSO);
    }
}
