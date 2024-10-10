using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavelinBullet : Weapon
{

    private Rigidbody rgd;
    private Collider col;

    protected override void Start()
    {
        base.Start();
        rgd = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    // 碰撞后处理
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == Tag.PLAYER) return;

        // 速度设置为0
        // rgd.velocity = Vector3.zero;
        // 不受力的影响
        rgd.isKinematic = true;
        // 禁用collider 别的物体不再碰撞此子弹
        col.enabled = false;

        // 命中的物体设置为标枪父物体
        transform.parent = other.gameObject.transform;
        if (other.gameObject.tag == Tag.ENEMY)
        {
           CalculateDamage(other.gameObject);
        }

        // 碰撞后一定时间进行销毁
        Destroy(this.gameObject, 2.0f);

    }
}
