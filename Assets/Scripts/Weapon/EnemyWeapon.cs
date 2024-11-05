using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : Weapon
{
    private const string ANIM_PARM_ISATTACK = "IsAttack";
    private Animator animator;
    protected override void Start()
    {
        animator = GetComponent<Animator>();
        owner = transform.parent.gameObject;
        ownerProperty = owner.GetComponent<EnemyProperty>();
        ownerIsPlayer = false;
    }

    public override void Attack()
    {
        animator.SetTrigger(ANIM_PARM_ISATTACK);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.PLAYER))
        {
            CalculateDamage(other.gameObject);
        }
    }
}
