using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacked : MonoBehaviour
{
    public float MaxHp = 100;
    public float HP = 100;
    public int exp = 20;
    
    void OnEnable()
    {
        HP = MaxHp;
        GetComponent<CapsuleCollider>().enabled = true;
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    { }

    public void TakeDamage(float damage)
    {
        HP -= damage;
        HP = Mathf.Clamp(HP, 0, MaxHp);

        if (HP <= 0)
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
