using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public PlayerProperty playerProperty;

    public virtual void Attack() { }

    protected virtual void Start()
    {
        playerProperty = GameObject.FindGameObjectWithTag(Tag.PLAYER).GetComponent<PlayerProperty>();
    }

    public float CalculateDamage()
    {
        //playerProperty = GameObject.FindGameObjectWithTag(Tag.PLAYER).GetComponent<PlayerProperty>();
        // TODO: 人物属性的影响
        float damage = 0;
        if (Utils.GetRandomRate() > playerProperty.criticalRate.value)
        {
            damage = playerProperty.attack.value;
        }
        else
        {
            damage = playerProperty.attack.value * (1 + playerProperty.criticalDamage.value / 100f);
        }
        return damage;
    }
}
