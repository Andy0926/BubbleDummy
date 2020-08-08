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

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(velocityX, velocityY);
        //GameObject.Destroy(gameObject,3f);
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rigidBody.velocity;
        timer++;
        if(timer==1000){
            //GameObject bubble = GameObject.FindGameObjectsWithTag("Bubble");
            Destroy(GameObject.FindWithTag("Bubble"));
        }
        //Destroy(gameObject, 5f); ;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Platform"))
        {
            Debug.Log("Reflect");

            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            Debug.Log(direction);
            rigidBody.velocity = direction * speed;
        }
    }
}
