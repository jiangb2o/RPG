using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Weapon : MonoBehaviour
{
    public bool ownerIsPlayer;
    public GameObject owner;
    public Property ownerProperty;
    public GameObject textPopupPrefab;
    public virtual void Attack() { }

    protected virtual void Start()
    {
        owner = GameObject.FindWithTag(Tag.PLAYER);
        ownerProperty = owner.GetComponent<PlayerProperty>();
        ownerIsPlayer = true;
    }

    public void CalculateDamage(GameObject target)
    {
        GameObject textPopupGameObject = Instantiate(textPopupPrefab, target.transform);
        PopupText popupText = textPopupGameObject.GetComponent<PopupText>();
        popupText.isEnemy = ownerIsPlayer;
        
        float damage = 0;
        if (Utils.GetRandomRate() > ownerProperty.criticalRate.value)
        {
            damage = ownerProperty.attack.value;
        }
        else
        {
            popupText.isCritical = true;
            damage = ownerProperty.attack.value * (1 + ownerProperty.criticalDamage.value / 100f);
        }
        popupText.damage = damage;

        if (ownerIsPlayer)
        {
            target.GetComponent<EnemyAttacked>().TakeDamage(damage);
        }
        else
        {
            target.GetComponent<PlayerProperty>().TakeDamage(damage);
        }
    }
}
