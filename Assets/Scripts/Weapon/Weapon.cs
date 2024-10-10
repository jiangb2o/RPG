using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public PlayerProperty playerProperty;
    public GameObject textPopupPrefab;
    public virtual void Attack() { }

    protected virtual void Start()
    {
        playerProperty = GameObject.FindGameObjectWithTag(Tag.PLAYER).GetComponent<PlayerProperty>();
    }

    public void CalculateDamage(GameObject target)
    {
        GameObject textPopupGameObject = Instantiate(textPopupPrefab, target.transform);
        
        float damage = 0;
        if (Utils.GetRandomRate() > playerProperty.criticalRate.value)
        {
            damage = playerProperty.attack.value;
        }
        else
        {
            textPopupGameObject.GetComponent<PopupText>().isCritical = true;
            damage = playerProperty.attack.value * (1 + playerProperty.criticalDamage.value / 100f);
        }
        target.GetComponent<EnemyAttacked>().TakeDamage(damage);
    }
}
