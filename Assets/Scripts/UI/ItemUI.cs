using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI typeText;

    public ItemScriptObject itemSO;

    void Start()
    {
        if(itemSO != null)
        {
            InitItem(itemSO);
        }
    }

    public void InitItem(ItemScriptObject itemSO)
    {
        this.itemSO = itemSO;

        iconImage.sprite = itemSO.icon;
        nameText.text = itemSO.itemName;
        typeText.text = Utils.ItemTypeToString(itemSO.itemType);
    }

    public void OnClick()
    {
        // 点击物品, 将物品SO传递回InventoryUI, 使其控制显示详情
        // 并传递此脚本, 便于删除挂载脚本的物体
        InventoryUI.Instance.ShowItemDetail(this);
    }


    /// Called every frame while the mouse is over the GUIElement or Collider.
    /// </summary>
    public void OnMouseEnter()
    {
        InventoryUI.Instance.ShowItemDetail(this);
    }

    // public void OnMouseExit() {
    //     InventoryUI.Instance.CloseItemDetail();
    // }
}
