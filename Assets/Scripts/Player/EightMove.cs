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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetKeys();

        if (keyCodeQueue.Contains(W))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        if (keyCodeQueue.Contains(S))
        {
            transform.position -= transform.forward * speed * Time.deltaTime;
        }
        if (keyCodeQueue.Contains(A))
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }
        if (keyCodeQueue.Contains(D))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
    }

    void GetKeys()
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

        if(Input.GetKeyUp(W))
        {
            RemoveKeyInQueue(W);
        }
        if(Input.GetKeyUp(A))
        {
            RemoveKeyInQueue(A);
        }
        if(Input.GetKeyUp(S))
        {
            RemoveKeyInQueue(S);
        }
        if(Input.GetKeyUp(D))
        {
            RemoveKeyInQueue(D);
        }

        keyCodeList = keyCodeQueue.ToList();
    }

    private void RemoveKeyInQueue(KeyCode k)
    {
        if(keyCodeQueue.Contains(k))
        {
            if(keyCodeQueue.Peek() == k)
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
