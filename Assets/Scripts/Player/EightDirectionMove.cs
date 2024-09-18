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

    public Queue<KeyCode> keyCodeQueue = new Queue<KeyCode>();
    public List<KeyCode> keyCodeList;

    private Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = 12;
        speed = 0;
        acceleration = 20;
        moveDir = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        GetKeys();
        UpdateMoveDirectionByKeys();

        if (moveDir != Vector3.zero)
        {
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
        else if(speed != 0)// 无按键输入且速度不为0, 做匀减速运动
        {
            AccelerationPhase(-acceleration, 0.0f);
        }
    }

    private void AccelerationPhase(float a, float limitSpeed)
    {
        float newSpeed = speed + a * Time.deltaTime;
        // 可以加速到最大速度/减速到最小速度
        if ( (a > 0 && newSpeed > limitSpeed) || (a < 0 && newSpeed < limitSpeed))
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

        if (keyCodeQueue.Contains(W))
        {
            moveDir += cameraForward;
        }
        if (keyCodeQueue.Contains(A))
        {
            moveDir -= cameraRight;
        }
        if (keyCodeQueue.Contains(S))
        {
            moveDir -= cameraForward;
        }
        if (keyCodeQueue.Contains(D))
        {
            moveDir += cameraRight;
        }

        moveDir.Normalize();
    }

    private void GetKeys()
    {
        if (Input.GetKeyDown(W))
        {
            AddKeyToQueue(W);
        }
        if (Input.GetKeyDown(A))
        {
            AddKeyToQueue(A);
        }
        if (Input.GetKeyDown(S))
        {
            AddKeyToQueue(S);
        }
        if (Input.GetKeyDown(D))
        {
            AddKeyToQueue(D);
        }

        if (Input.GetKeyUp(W))
        {
            RemoveKeyInQueue(W);
        }
        if (Input.GetKeyUp(A))
        {
            RemoveKeyInQueue(A);
        }
        if (Input.GetKeyUp(S))
        {
            RemoveKeyInQueue(S);
        }
        if (Input.GetKeyUp(D))
        {
            RemoveKeyInQueue(D);
        }

        keyCodeList = keyCodeQueue.ToList();
    }

    private void RemoveKeyInQueue(KeyCode k)
    {
        if (keyCodeQueue.Contains(k))
        {
            if (keyCodeQueue.Peek() == k)
            {
                keyCodeQueue.Dequeue();
            }
            else // 包含k, 但队首不是k
            {
                // 将原队首移至队尾
                keyCodeQueue.Enqueue(keyCodeQueue.Dequeue());
                keyCodeQueue.Dequeue();
            }
        }
    }

    private void AddKeyToQueue(KeyCode k)
    {
        if (keyCodeQueue.Count >= 2)
        {
            // 不包含当前keyCode或已包含且在队首
            if (!keyCodeQueue.Contains(k) || keyCodeQueue.Peek() == k)
            {
                // 将当前keyCode加入队尾
                keyCodeQueue.Dequeue();
                keyCodeQueue.Enqueue(k);
            }
        }
        else
        {
            if (!keyCodeQueue.Contains(k))
            {
                keyCodeQueue.Enqueue(k);
            }
        }
    }
}
