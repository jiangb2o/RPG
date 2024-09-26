using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EightDirectionMove : MonoBehaviour
{
    const KeyCode W = KeyCode.W;
    const KeyCode A = KeyCode.A;
    const KeyCode S = KeyCode.S;
    const KeyCode D = KeyCode.D;

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
        acceleration = 20;
        moveDir = Vector3.zero;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMoveDirectionByKeys();

        if (moveDir != Vector3.zero)
        {
            animator.SetBool("isRun", true);
            transform.forward = moveDir;
            // 匀速阶段
            if (speed >= maxSpeed)
            {
                transform.position += transform.forward * speed * Time.deltaTime;
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
            if (speed < 5)
            {
                animator.SetBool("isRun", false);
            }
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
            transform.position += transform.forward * (speed + limitSpeed) / 2 * accTime;
            // 匀速阶段距离
            transform.position += transform.forward * limitSpeed * (Time.deltaTime - accTime);
            speed = limitSpeed;
        }
        // 一直匀加速/匀减速
        else
        {
            transform.position += transform.forward * (speed + newSpeed) / 2 * Time.deltaTime;
            speed = newSpeed;
        }
    }

    private void UpdateMoveDirectionByKeys()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0.0f;
        Vector3 cameraRight = Camera.main.transform.right;

        moveDir = Vector3.zero;

        if (Input.GetKey(W))
        {
            moveDir += cameraForward;
        }
        if (Input.GetKey(A))
        {
            moveDir -= cameraRight;
        }
        if (Input.GetKey(S))
        {
            moveDir -= cameraForward;
        }
        if (Input.GetKey(D))
        {
            moveDir += cameraRight;
        }

        moveDir.Normalize();
    }
}
