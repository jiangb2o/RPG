using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Weapon weapon;
    public ItemScriptObject weaponSO;
    private PlayerProperty playerProperty;

    public void LoadWeapon(ItemScriptObject itemSO)
    {
        if (weapon != null)
        {
            UnloadWeapon();
            // 移除原武器属性值
            playerProperty.RemoveProperty(weaponSO.itemName);
        }

        weaponSO = itemSO;
        GameObject weaponPrefab = itemSO.prefab;
        Transform weaponSite = transform.Find(weaponPrefab.name + "Site");
        GameObject weaponGameObject = GameObject.Instantiate(weaponPrefab, weaponSite);
        weapon = weaponGameObject.GetComponent<Weapon>();

    }

    public void UnloadWeapon()
    {
        if (weaponSO != null)
        {
            InventoryManager.Instance.AddItem(weaponSO);
            Destroy(weapon.gameObject);
            weapon = null;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerProperty = GetComponent<PlayerProperty>();
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon != null && Input.GetKeyDown(KeyCode.Space))
        {
            weapon.Attack();
        }
    }
}
