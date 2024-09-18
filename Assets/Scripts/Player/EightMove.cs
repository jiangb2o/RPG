using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EightMove : MonoBehaviour
{
    const KeyCode W = KeyCode.W;
    const KeyCode A = KeyCode.A;
    const KeyCode S = KeyCode.S;
    const KeyCode D = KeyCode.D;

    public float speed = 10;
    public Queue<KeyCode> keyCodeQueue = new Queue<KeyCode>();
    [SerializeField] public List<KeyCode> keyCodeList;

    private Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        moveDir = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        GetKeys();
        UpdateMoveDirectionByKeys();

        if(moveDir != Vector3.zero)
        {
            transform.forward = moveDir;
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    private void UpdateMoveDirectionByKeys()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0.0f;
        Vector3 cameraRight = Camera.main.transform.right;

        moveDir = Vector3.zero;

        if (keyCodeQueue.Count == 2)
        {
            if(keyCodeQueue.Contains(W) && keyCodeQueue.Contains(A))
            {
                moveDir = Vector3.Normalize(cameraForward - cameraRight);
            }
            else if(keyCodeQueue.Contains(W) && keyCodeQueue.Contains(D))
            {
                moveDir = Vector3.Normalize(cameraForward + cameraRight);
            }
            else if(keyCodeQueue.Contains(S) && keyCodeQueue.Contains(A))
            {
                moveDir = Vector3.Normalize(-cameraForward - cameraRight);
            }
            else if(keyCodeQueue.Contains(S) && keyCodeQueue.Contains(D))
            {
                moveDir = Vector3.Normalize(-cameraForward + cameraRight);
            }
            // WS, AD 不移动
        }
        else if(keyCodeQueue.Count == 1)
        {
            if (keyCodeQueue.Contains(W))
            {
                moveDir = cameraForward;
            }
            else if (keyCodeQueue.Contains(S))
            {
                moveDir = -cameraForward;
            }
            else if (keyCodeQueue.Contains(A))
            {
                moveDir = -cameraRight;
            }
            else if (keyCodeQueue.Contains(D))
            {
                moveDir = cameraRight;
            }
        }
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
