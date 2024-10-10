using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{

    public float maxSpeed;
    public float speed;
    public float acceleration;

    private Vector3 moveDir;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = 10;
        speed = 0;
        acceleration = 70;
        moveDir = Vector3.zero;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMoveDirection();

        if (moveDir != Vector3.zero)
        {
            //if(animator) animator.SetBool("IsRun", true);
            transform.LookAt(transform.position + moveDir);
            // 匀速阶段
            if (speed >= maxSpeed)
            {
                transform.Translate(0, 0, speed * Time.deltaTime);
            }
            // 继续加速
            else
            {
                AccelerationPhase(acceleration, maxSpeed);
            }
        }
        else
        {
            if (speed != 0)// 无按键输入且速度不为0, 做匀减速运动
            {
                AccelerationPhase(-acceleration, 0.0f);
            }
            // if (speed < 5)
            // {
            //     if(animator) animator.SetBool("IsRun", false);
            // }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != Tag.GROUND)
        {
            speed = 0;
        }
    }

    private void AccelerationPhase(float a, float limitSpeed)
    {
        float newSpeed = speed + a * Time.deltaTime;
        // 可以加速到最大速度/减速到最小速度
        if ((a > 0 && newSpeed > limitSpeed) || (a < 0 && newSpeed < limitSpeed))
        {
            float accTime = Mathf.Abs((limitSpeed - speed) / a);
            // 加速/减速阶段距离
            transform.Translate(0, 0, (speed + limitSpeed) / 2 * accTime);
            // 匀速阶段距离
            transform.Translate(0, 0, limitSpeed * (Time.deltaTime - accTime));
            speed = limitSpeed;
        }
        // 一直匀加速/匀减速
        else
        {
            transform.Translate(0, 0, (speed + newSpeed) / 2 * Time.deltaTime);
            speed = newSpeed;
        }
    }

    private void UpdateMoveDirection()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0.0f;
        Vector3 cameraRight = Camera.main.transform.right;
        
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        if (animator)
        {
            animator.SetFloat("MoveX", moveX);
            animator.SetFloat("MoveY", moveY);
        }
        // 相对摄像机方向
        moveDir = moveX * cameraRight + moveY * cameraForward;
        moveDir.Normalize();
    }
}
