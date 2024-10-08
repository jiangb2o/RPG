using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimatorController : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Attack()
    {
        //animator.SetBool("isAttack", true);
        animator.SetTrigger("T_Attack");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            Attack();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("T_Jump");
        }
    }
}
