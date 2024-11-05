using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "DataScriptObject")]
public class ItemScriptObject : ScriptableObject
{
    public int id;
    public string itemName;
    public ItemType itemType;
    public string description;
    public List<PropertyPairs> propertyList;
    public Sprite icon;
    public GameObject prefab;

}


