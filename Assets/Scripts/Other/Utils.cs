using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static string ItemTypeToString(ItemType itemType)
    {
        string typeString = "";
        switch (itemType)
        {
            case ItemType.Weapon : typeString = "武器"; break;
            case ItemType.Consumable: typeString = "消耗品"; break;
        }

        return typeString;
    }

    public static string ItemPropertyTypeToString(PropertyType itemPropertyType)
    {
        string itemPropertyString = "";
        switch (itemPropertyType)
        {
            case PropertyType.HP : itemPropertyString = "生命"; break;
            case PropertyType.Speed: itemPropertyString = "速度"; break;
            case PropertyType.Attack: itemPropertyString = "攻击力"; break;
            case PropertyType.CriticalRate: itemPropertyString = "暴击率"; break;
            case PropertyType.CriticalDamage: itemPropertyString = "暴击伤害"; break;
        }

        return itemPropertyString;
    }

    public static Color ItemPropertyTypeColor(PropertyType itemPropertyType)
    {
        Color color = Color.black;
        switch (itemPropertyType)
        {
            case PropertyType.HP : color = Color.green; break;
            case PropertyType.Speed: color = Color.blue; break;
            case PropertyType.Attack: color = Color.red; break;
            case PropertyType.CriticalRate: color = new Color(0.7058f, 0.2705f, 0f); break;
            case PropertyType.CriticalDamage: color = new Color(0.7058f, 0.2705f, 0f); break;
        }

        return color;
    }

    public static float GetRandomRate()
    {
        return Random.value * 100;
    }

    public static void ClearGrid(Transform transform)
    {
        foreach (Transform child in transform)
        {
            if(child.gameObject.activeSelf)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public static float consumableDuration = 20.0f;
}

public enum ItemType
{
    Weapon,
    Consumable,
}

[System.Serializable]
public class PropertyPairs
{
    public PropertyType propertyType;
    public float value;
}

public enum PropertyType
{
    HP,
    Speed,
    Attack,
    CriticalRate,
    CriticalDamage,
}

public enum GameTaskState
{
    Waiting,
    Executing,
    Completed,
    End,
}