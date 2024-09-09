using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavelinWeapon : Weapon
{
    public float bulletSpeed;
    // 子弹预设
    public GameObject bulletPrefab;

    private GameObject bulletGO;

    protected override void Start()
    {
        base.Start();
        SpawnBullet();
    }

    public override void Attack()
    {
        if (bulletGO != null)
        {
            // 发射后将bullet父物体置空
            bulletGO.transform.parent = null;
            bulletGO.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
            // 发射后启用collider进行碰撞检测
            bulletGO.GetComponent<Collider>().enabled = true;
            // 未发生碰撞则一段时间后销毁
            Destroy(bulletGO, 7.0f);
            bulletGO = null;
            Invoke("SpawnBullet", 0.5f);
        }
        else
        {
            return;
        }
    }

    private void SpawnBullet()
    {
        // 设置bullet父物体为JavelinWeapon 使坐标跟随 JavelinWeapon 坐标
        bulletGO = GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation, transform);
        // 未发射时禁用collider 防止自动销毁
        bulletGO.GetComponent<Collider>().enabled = false;

        // 作为掉落物的处理
        if(tag == Tag.INTERACTABLE)
        {
            // 移除作为子弹时的组件(防止碰撞后子弹自动销毁)
            Destroy(bulletGO.GetComponent<JavelinBullet>());

            bulletGO.tag = Tag.INTERACTABLE;
            // 为子弹挂载PickableObject组件, 并将itemSO设置为自身PickableObject的对象
            PickableObject po = bulletGO.AddComponent<PickableObject>();
            po.itemSO = this.GetComponent<PickableObject>().itemSO;

            // 设置子弹的rigidbody属性
            Rigidbody rigidbody = bulletGO.GetComponent<Rigidbody>();
            rigidbody.constraints = RigidbodyConstraints.None;
            rigidbody.useGravity = true;

            bulletGO.GetComponent<Collider>().enabled = true;

            // 作为Pickable时不需要父物体
            bulletGO.transform.parent = null;
            Destroy(this.gameObject);
        }
    }
}

