using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build;
using UnityEditorInternal;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Collider2D myCollider2d;
    [SerializeField] float runSpeed = 1.5f;
    [SerializeField] float jumpSpeed = 10.0f;
    [SerializeField] private LayerMask platformLayerMask;

    public GameObject projectileLeft, projectileRight;
    Vector2 projectilePosition;

    //Projectile
    public float fireRate = 0.5f;
    float nextFire = 0;
    bool facingRight = true;

    //Combat
    public int health = 3;
    public float invincibleTimeAfterHurt = 2;



    private bool isGrounded;
    void Start()
    {
        myRigidBody = transform.GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2d = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Walk();
        FlipSprite();
        fire();
    }

    private void Walk()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); //value is between -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool walking = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Walking", walking);
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            facingRight = true;
            Debug.Log("right");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
         
            facingRight = false;
            Debug.Log("left");
        }

    }

    private void FlipSprite()
    {
        //if the player is moving horizontally
        bool velocity = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if(velocity)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x),1f);
            
            // reverse the current scaling of x axis
        }
    }
    private void Jump()
    {
        if (!myCollider2d.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetLayerWeight(1, 0);
            myAnimator.SetTrigger("Iddling");

            Debug.Log("Touching Layer");
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 jumpVelocityToAdd = new Vector2(5f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
            myAnimator.SetLayerWeight(1, 1);
            myAnimator.SetTrigger("Jumping");
        }
    }

    void fire()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            projectilePosition = transform.position;
            if (facingRight)
            {
                projectilePosition += new Vector2(+0.1f, 0);
                Instantiate(projectileRight, projectilePosition, Quaternion.identity);
                Debug.Log(projectilePosition);
            }
            else
            {
                projectilePosition += new Vector2(-0.1f, 0);
                Instantiate(projectileLeft, projectilePosition, Quaternion.identity);
                Debug.Log(projectilePosition);
            }
        }
    }
    void Hurt()
    {
        health--;
        /*
        if (health <= 0)
            Application.LoadLevel(Application.loadedLevel);
        */
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        if (enemy!=null)
        {
            Hurt();
        }
    }
}
