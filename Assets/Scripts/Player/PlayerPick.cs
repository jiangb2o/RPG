using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPick : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        // 先检测是否为可交互物体
        if (other.gameObject.tag == Tag.INTERACTABLE)
        {
            PickableObject po = other.gameObject.GetComponent<PickableObject>();
            // 检测是否有PickableObject组件
            if (po != null)
            {
                // 添加到仓库并销毁物体
                InventoryManager.Instance.AddItem(po.itemSO);
                Destroy(po.gameObject);
            }
        }
    }

}
