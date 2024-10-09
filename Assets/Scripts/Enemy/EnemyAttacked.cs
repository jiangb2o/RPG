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
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    { }

    public void TakeDamage(float damage)
    {
        HP -= damage;

        if (HP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        DropPickableItem();
        EventCenter.EnemyDied(this);
    }

    private void DropPickableItem()
    {
        GetComponent<Collider>().enabled = false;
        int dropNumber = Random.Range(0, 4);

        for (int i = 0; i < dropNumber; ++i)
        {
            ItemScriptObject itemSO = ItemManager.Instance.GetRandomItem();

            GameObject ItemObject = GameObject.Instantiate(itemSO.prefab, transform.position + 2 * Vector3.up, Quaternion.identity);
            ItemObject.tag = Tag.INTERACTABLE;
            // 动画组件影响生成位置, 禁用动画组件
            Animator anim = ItemObject.GetComponent<Animator>();
            if (anim != null)
            {
                anim.enabled = false;
            }
            // 挂载PickableObject脚本, 并赋值物体类别
            PickableObject PO = ItemObject.AddComponent<PickableObject>();
            PO.itemSO = itemSO;

            // 设置武器 prefab 的collider 属性
            Collider collider = ItemObject.GetComponent<Collider>();
            
            if (collider != null)
            {
                collider.enabled = true;
                collider.isTrigger = false;
            }

            // 设置武器 prefab 的 rigidbody属性
            Rigidbody rigidbody = ItemObject.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.useGravity = true;
                rigidbody.isKinematic = false;
            }
        }

    }
}
