using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleProjectile : MonoBehaviour
{
    public float velocityX = 5f;
    public float velocityY = 0f;
    Rigidbody2D rigidBody;
    Vector3 lastVelocity;
    public int timer =1;
    public static bool reflectedBullet = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(velocityX, velocityY);
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rigidBody.velocity;
        timer++;
        if(timer==800){
            
            Destroy(GameObject.FindWithTag("Bubble"));
            BubbleProjectile.reflectedBullet = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Platform")|| collision.gameObject.CompareTag("Trigger")|| collision.gameObject.CompareTag("Bubble") || collision.gameObject.CompareTag("Lock1"))
        {
            //Debug.Log("Reflect");
            reflectedBullet = true;
            SoundManagerScript.PlaySound("Reflect");
            var speed = lastVelocity.magnitude+0.5f;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            //Debug.Log(direction);
            rigidBody.velocity = direction * speed;
        }
        else
        {
            //reflectedBullet = false;
        }
    }
}
