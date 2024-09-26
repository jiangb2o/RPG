using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private float maxHeight = 5f;
    private float speed = 3f;

    public bool fallGround;
    public bool isfalling;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        fallGround = true;
        isfalling = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && fallGround)
        {
            fallGround = false;
            animator.SetTrigger("T_Jump");
            animator.SetBool("FallGround", fallGround);
        }
        if (Input.GetKey(KeyCode.Space) && !isfalling && !fallGround)
        {
            Vector3 position = transform.position;
            position.y = Mathf.Min(maxHeight, position.y + speed * Time.deltaTime);
            transform.position = position;
        }

        if(Input.GetKeyUp(KeyCode.Space) && !fallGround)
        {
            isfalling = true;
        }
        else if(!fallGround && transform.position.y >= 5f)
        {
            isfalling = true;
        }

        if(isfalling && !fallGround)
        {
            Vector3 position = transform.position;
            position.y = Mathf.Max(0f, position.y - speed * Time.deltaTime);
            transform.position = position;
        }
        if(transform.position.y <= 0.8)
        {
            isfalling = false;
            fallGround = true;
            animator.SetBool("FallGround", fallGround);
        }
    }
}
