using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    public float attackDistance = 1f;
    public Weapon weapon;
    
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        weapon = transform.Find("Weapon").GetComponent<EnemyWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InAttackDistance())
        {
            weapon.Attack();
        }
    }

    private bool InAttackDistance() => Vector3.Distance(transform.position, playerTransform.position) < attackDistance;
}
