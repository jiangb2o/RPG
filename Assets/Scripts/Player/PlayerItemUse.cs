using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Buff
{
    public ItemScriptObject itemSO;
    public float currentDuration;

    public Buff(ItemScriptObject itemSO)
    {
        this.itemSO = itemSO;
        currentDuration = 0.0f;
    }
}

public class PlayerItemUse : MonoBehaviour
{
    private PlayerAttack playerAttack;
    private PlayerProperty playerProperty;
    public List<Buff> buffList;

    void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
        playerProperty = GetComponent<PlayerProperty>();

        buffList = new List<Buff>();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        for (int i = 0; i < buffList.Count; ++i)
        {
            buffList[i].currentDuration += Time.deltaTime;
            if(buffList[i].currentDuration > Utils.consumableDuration)
            {
                playerProperty.RemoveProperty(buffList[i].itemSO.itemName);
                buffList.Remove(buffList[i]);
            }
        }
    }

    public void UseItem(ItemScriptObject itemSO)
    {
        if(itemSO.itemType == ItemType.Weapon)
        {
            // 替换武器时要移除原武器的属性值
            playerAttack.LoadWeapon(itemSO);
        }
        else if(itemSO.itemType == ItemType.Consumable)
        {
            // 已经存在, 刷新时间
            foreach(var buff in buffList)
            {
                if(buff.itemSO == itemSO) 
                {
                    buff.currentDuration = 0;
                    return;
                }
            }
            // 不存在, 添加buff
            buffList.Add(new Buff(itemSO));
        }
        playerProperty.AddProperties(itemSO.propertyList, itemSO.itemName);
    }

}
