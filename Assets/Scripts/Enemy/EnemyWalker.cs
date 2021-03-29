using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class EnemyWalker : MonoBehaviour
{

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    public int health;
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
       sr= GetComponent<SpriteRenderer>();
       anim = GetComponent<Animator>();

        if (Speed <= 0)
        {
            Speed = 2.0f;
        }

        if (health <=0 )
        {
            health = 3;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!anim.GetBool("Death"))
        {
            if (sr.flipX)
            {
                rb.velocity = new Vector2(-Speed, rb.velocity.y);

            }
            else
            {
                rb.velocity = new Vector2(Speed, rb.velocity.y);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            sr.flipX = !sr.flipX;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Destroy(GetComponent<Collider>().gameObject);
            Destroy(gameObject);
        }

    }
    public void IsDead()
    {
        health--;
        if (health <= 0)
        {
            anim.SetBool("Death", true);
                rb.velocity = Vector2.zero;
        }
    }

 
    public void FinishedDeath()
    {
        Destroy(gameObject);
    }
}
