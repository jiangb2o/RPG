using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Scripting;

public class ItemManager : MonoSingleton<ItemManager>
{
    public ItemDatabase itemDB;
    public ItemScriptObject GetRandomItem()
    {
        int index = Random.Range(0, itemDB.itemList.Count);
        return itemDB.itemList[index];
    }

    public void DropPickableItem(Vector3 position)
    {
        int dropNumber = Random.Range(0, 4);

        for (int i = 0; i < dropNumber; ++i)
        {
            ItemScriptObject itemSO = Instance.GetRandomItem();

            GameObject ItemObject = GameObject.Instantiate(itemSO.prefab, position + 2 * Vector3.up, Quaternion.identity);
            ItemObject.tag = Tag.INTERACTABLE;
            // 动画组件影响生成位置, 禁用动画组件
            Animator anim = ItemObject.GetComponent<Animator>();
            if (anim != null)
            {
                anim.enabled = false;
            }
            // 挂载PickableObject脚本, 并赋值物体类别
            PickableObject PO = ItemObject.AddComponent<PickableObject>();
            PO.itemSO = itemSO;

            // 设置武器 prefab 的collider 属性
            Collider collider = ItemObject.GetComponent<Collider>();
            
            if (collider != null)
            {
                collider.enabled = true;
                collider.isTrigger = false;
            }

            // 设置武器 prefab 的 rigidbody属性
            Rigidbody rigidbody = ItemObject.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.useGravity = true;
                rigidbody.isKinematic = false;
            }
        }

    }

}
