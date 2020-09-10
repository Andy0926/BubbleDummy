using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEnemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidBody2D;
    Collider2D myCollider2d;
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myCollider2d = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (IsFacingRight())
        {
            myRigidBody2D.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            myRigidBody2D.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody2D.velocity.x)), 1f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Bubble"))
        {
            SoundManagerScript.PlaySound("Hit");
            if (BubbleProjectile.reflectedBullet==true)
            {
                Debug.Log("HitSpecialEnemy");
                
                Destroy(gameObject);
                BubbleProjectile.reflectedBullet = false;
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
