using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.PostProcessing;

public class PropertyRecord
{
    public string itemName;
    public int value;
    public PropertyRecord(){}

    public PropertyRecord(string itemName, int value)
    {
        this.itemName = itemName;
        this.value = value;
    }

}

public class PlayerProperty : MonoBehaviour
{
    public Dictionary<PropertyType, List<PropertyRecord>> propertyDict;
    //public int hp, speed, criticalRate, criticalDamage, attack;
    public int hpMax = 100, speedMin = 3;
    public Property hp, speed, criticalRate, criticalDamage, attack;

    private List<PropertyRecord> propertyRecordList;

    private NavMeshAgent playerAgent;
    private CharacterMove playMove;

    // Start is called before the first frame update
    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        playMove = GetComponent<CharacterMove>();

        hp.propertyType = PropertyType.HP;
        attack.propertyType = PropertyType.Attack;
        speed.propertyType  = PropertyType.Speed;
        criticalRate.propertyType = PropertyType.CriticalRate;
        criticalDamage.propertyType = PropertyType.CriticalDamage;

        propertyDict = new Dictionary<PropertyType, List<PropertyRecord>>();
        foreach (PropertyType propertyType in Enum.GetValues(typeof(PropertyType)))
        {
            // hp 单独处理
            if(propertyType == PropertyType.HP) continue;
            propertyDict.Add(propertyType, new List<PropertyRecord>());
        }

        // base properties
        hp.value = hpMax;
        AddProperty(PropertyType.Speed, "base", 10);
        AddProperty(PropertyType.CriticalRate, "base", 5);
        AddProperty(PropertyType.CriticalDamage, "base", 50);
        UpdateProperty();
    }

    void Update()
    {
        
    }

    void UpdateProperty()
    {
        speed.value = 0; criticalRate.value = 0; criticalDamage.value = 0; attack.value = 0;
        foreach (PropertyType propertyType in Enum.GetValues(typeof(PropertyType)))
        {
            propertyDict.TryGetValue(propertyType, out propertyRecordList);
            if(propertyType == PropertyType.Attack)
            {
                foreach( PropertyRecord propertyRecord in propertyRecordList)
                {
                    attack.value += propertyRecord.value;
                }
            }
            else if(propertyType == PropertyType.Speed)
            {
                foreach( PropertyRecord propertyRecord in propertyRecordList)
                {
                    speed.value += propertyRecord.value;
                }
            }
            else if(propertyType == PropertyType.CriticalRate)
            {
                foreach( PropertyRecord propertyRecord in propertyRecordList)
                {
                    criticalRate.value += propertyRecord.value;
                }
            }
            else if(propertyType == PropertyType.CriticalDamage)
            {
                foreach( PropertyRecord propertyRecord in propertyRecordList)
                {
                    criticalDamage.value += propertyRecord.value;
                }
            }
        }
        speed.value = Math.Max(speed.value, speedMin);
        if(playerAgent != null)
        {
            playerAgent.speed = speed.value;
        }
        else if (playMove != null)
        {
            playMove.maxSpeed = speed.value;
        }
        PlayerPropertyUI.Instance.UpdatePlayerPropertyUI();
    }

    public void AddProperties(List<Property> properties, string itemName)
    {
        foreach (var property in properties)
        {
            if(property.propertyType == PropertyType.HP)
            {
                AddHp(property.value);
            }
            else
            {
                AddProperty(property.propertyType, itemName, property.value);
            }
        }
        UpdateProperty();
    }

    void AddHp(int value)
    {
        hp.value = Math.Min(hp.value + value, hpMax);
    }

    public void UpdateMaxHp(int level)
    {
        hpMax = 100 + (level - 1) * 50;
        hp.value = hpMax;
    }

    void AddProperty(PropertyType propertyType, string itemName ,int value)
    {
        // 符合属性类的 属性记录List
        propertyDict.TryGetValue(propertyType, out propertyRecordList);
        propertyRecordList.Add(new PropertyRecord(itemName, value));

    }

    public void RemoveProperty(string itemName)
    {
        // 遍历所有property类型
        foreach (PropertyType propertyType in Enum.GetValues(typeof(PropertyType)))
        {
            // 血量为一次性添加, 移除后不会减少
            if(propertyType == PropertyType.HP) 
            {
                continue;
            }
            propertyDict.TryGetValue(propertyType, out propertyRecordList);
            // 移除List中物体名称属性值
            propertyRecordList.Remove(propertyRecordList.Find(x => x.itemName == itemName));
        }
        UpdateProperty();
    }
}
