using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimatorController : MonoBehaviour
{
    private Animator animator;
    public Button attackButton;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        attackButton.onClick.AddListener(Attack);
    }

    private void Attack()
    {
        //animator.SetBool("isAttack", true);
        animator.SetTrigger("T_isAttack");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            Attack();
        }
    }
}
