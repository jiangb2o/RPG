using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPropertyUI : MonoBehaviour
{
    public static PlayerPropertyUI Instance { get; private set; }

    private Image hpProgressBar;
    private Image levelProgressBar;
    private TextMeshProUGUI hpText;
    private TextMeshProUGUI levelText;

    private GameObject propertyGrid;
    private Image weaponIcon;
    public Sprite unWeaponSprite;

    public GameObject propertyTemplate;

    private PlayerProperty playerProperty;
    private PlayerLevel playerLevel;
    private PlayerAttack playerAttack;

    private GameObject uiGameObject;

    private bool isShow = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        string prefix = "UI/Background/";
        hpProgressBar = transform.Find(prefix + "HP/ProgressBar").GetComponent<Image>();
        levelProgressBar = transform.Find(prefix + "Level/ProgressBar").GetComponent<Image>();

        hpText = transform.Find(prefix + "HP/Value").GetComponent<TextMeshProUGUI>();
        levelText = transform.Find(prefix + "Level/Value").GetComponent<TextMeshProUGUI>();

        propertyGrid = transform.Find(prefix + "PropertyLayout").gameObject;
        weaponIcon = transform.Find(prefix + "EquipWeapon").GetComponent<Image>();

        GameObject player = GameObject.FindGameObjectWithTag(Tag.PLAYER);

        playerProperty = player.GetComponent<PlayerProperty>();
        playerLevel = player.GetComponent<PlayerLevel>();
        playerAttack = player.GetComponent<PlayerAttack>();

        uiGameObject = transform.Find("UI").gameObject;
        Hide();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isShow) Hide();
            else Show();
            isShow = !isShow;
        }
    }

    void Show()
    {
        UpdatePlayerPropertyUI();
        uiGameObject.SetActive(true);
    }

    void Hide()
    {
        uiGameObject.SetActive(false);
    }

    public void UpdatePlayerPropertyUI()
    {
        hpProgressBar.fillAmount = playerProperty.hp.value * 1.0f / playerProperty.hpMax;
        hpText.text = playerProperty.hp.value + "/" + playerProperty.hpMax;

        levelProgressBar.fillAmount = playerLevel.currentExp / (playerLevel.level * 30.0f);
        levelText.text = playerLevel.level.ToString();

        Utils.ClearGrid(propertyGrid.transform);

        UpdateSingleProperty(playerProperty.attack);
        UpdateSingleProperty(playerProperty.speed);
        UpdateSingleProperty(playerProperty.criticalRate);
        UpdateSingleProperty(playerProperty.criticalDamage);

        if (playerAttack.weapon != null)
        {
            weaponIcon.sprite = playerAttack.weaponSO.icon;
        }
        else
        {
            weaponIcon.sprite = unWeaponSprite;
        }
    }

    private void UpdateSingleProperty(Property property)
    {
        string propertyString = "";
        propertyString += Utils.ItemPropertyTypeToString(property.propertyType);
        propertyString += '\n';
        propertyString += property.value.ToString();

        if (property.propertyType == PropertyType.CriticalRate || property.propertyType == PropertyType.CriticalDamage)
        {
            propertyString += '%';
        }

        GameObject propertyCell = GameObject.Instantiate(propertyTemplate, propertyGrid.transform);

        TextMeshProUGUI propertyText = propertyCell.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        propertyText.text = propertyString;
        propertyText.color = Utils.ItemPropertyTypeColor(property.propertyType);
    }
}
