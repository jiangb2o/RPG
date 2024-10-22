using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : InteractableObject
{
    // 掉落物品时挂载此脚本并为itemSO赋值
    public ItemScriptObject itemSO;

    protected override void Interact()
    {
        // add to inventory and destroy
        InventoryManager.Instance.AddItem(itemSO);
        Destroy(gameObject);
    }
}
