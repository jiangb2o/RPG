using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailUI : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI descriptionText;
    public GameObject propertyGrid;

    public GameObject propertyTemplate;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide() 
    {
        Utils.ClearGrid(propertyGrid.transform);
        gameObject.SetActive(false);
    }


    public void UpdateItemDetailUI(ItemScriptObject itemSO)
    {
        // property of each item may not equal number
        // clear last item property before update
        Utils.ClearGrid(propertyGrid.transform);

        // icon, name, type and description is fixed number, just assignment new value
        iconImage.sprite = itemSO.icon;
        nameText.text = itemSO.itemName;
        typeText.text = Utils.ItemTypeToString(itemSO.itemType);
        descriptionText.text = itemSO.description;

        // create property template and set text and color for each property 
        foreach (Property itemProperty in itemSO.propertyList)
        {
            string propertyString = "";
            propertyString += Utils.ItemPropertyTypeToString(itemProperty.propertyType);
            propertyString += '\n';
            propertyString += itemProperty.value.ToString();
            if(itemProperty.propertyType == PropertyType.CriticalRate || itemProperty.propertyType == PropertyType.CriticalDamage)
            {
                propertyString += '%';
            }

            GameObject property = GameObject.Instantiate(propertyTemplate, transform.Find("PropertyLayout"));

            TextMeshProUGUI propertyText = property.transform.Find("Text").GetComponent<TextMeshProUGUI>();
            propertyText.text = propertyString;
            propertyText.color = Utils.ItemPropertyTypeColor(itemProperty.propertyType);
        }
    }

    

    public void OnUseButtonClick()
    {
        InventoryUI.Instance.OnItemUse();
    }
}
