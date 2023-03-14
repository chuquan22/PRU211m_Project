using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float maxSpeed;
    public float jumpHeight;
    Rigidbody2D mybody;

    bool facingRight;
    bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        mybody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        facingRight = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(move));
        mybody.velocity = new Vector2 (move * maxSpeed, mybody.velocity.y);
        if(move > 0 && !facingRight)
        {
            flip();
        } else if(move < 0 && facingRight)
        {
            flip();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (grounded)
            {
                grounded = false;
                animator.SetTrigger("isJumping");
                mybody.velocity = new Vector2(mybody.velocity.x, jumpHeight);

            }

        }

    }

    void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;    
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}
