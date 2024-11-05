using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacked : MonoBehaviour
{
    public EnemyProperty enemyProperty;
    public int exp = 20;
    void Awake()
    {
        enemyProperty = GetComponent<EnemyProperty>();
    }
    
    void OnEnable()
    {
        enemyProperty.hp.value = enemyProperty.hpMax;
        GetComponent<CapsuleCollider>().enabled = true;
    }
    

    // Update is called once per frame
    void Update()
    { }

    public void TakeDamage(float damage)
    {
        enemyProperty.hp.value -= damage;
        enemyProperty.hp.value = Mathf.Clamp(enemyProperty.hp.value, 0, enemyProperty.hpMax);

        if (enemyProperty.hp.value <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        ItemManager.Instance.DropPickableItem(transform.position);
        gameObject.GetComponent<EnemyDie>().enabled = true;
        EventCenter.EnemyDied(this);
    }
}
