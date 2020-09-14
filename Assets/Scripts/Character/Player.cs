using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEditorInternal;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Collider2D myCollider2d;
    [SerializeField] float runSpeed = 1.5f;
    [SerializeField] float jumpSpeed = 10.0f;
    //BoxCollider2D

    public GameObject projectileLeft, projectileRight;
    Vector2 projectilePosition;

    //Projectile
    public float fireRate = 0.5f;
    float nextFire = 0;
    bool facingLeft = true;

    //Combat
    public static int health = 3;
    public float invincibleTimeAfterHurt = 2;

    float velX;
    float velY;

    void Start()
    {
        myRigidBody = transform.GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2d = GetComponent<Collider2D>();
        health = 3;
        Debug.Log(health);
        //distToGround = myCollider2d.bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Walk();
        FlipSprite();
        fire();
        //Debug.Log(IsGrounded()+"here");
    }

    private void Walk()
    {   
        velX = Input.GetAxisRaw("Horizontal");
        if (velX ==1)
        {
            facingLeft = false;
            myAnimator.SetBool("Walking", true);
        }
        else if (velX == -1)
        {
            facingLeft = true;
            myAnimator.SetBool("Walking", true);
        }
        else
        {
            myAnimator.SetBool("Walking", false);
        }
        velY = myRigidBody.velocity.y;
        myRigidBody.velocity = new Vector2(velX*runSpeed,velY);   
    }

    private void FlipSprite()
    {
        //if the player is moving horizontally
        bool velocity = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if(velocity)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x),1f) ;          
            // reverse the current scaling of x axis
        }
    }
    private void Jump()
    {
        //Dont allow jump while in air

        if (myCollider2d.IsTouchingLayers(LayerMask.GetMask("Movable"))|| myCollider2d.IsTouchingLayers(LayerMask.GetMask("Trigger")))
        {
            //Debug.Log("Touching Movable Block@");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Sppace");
                SoundManagerScript.PlaySound("Jump");
                Vector2 jumpVelocityToAdd = new Vector2(5f, jumpSpeed);
                myRigidBody.velocity += jumpVelocityToAdd;
                JumpAnimation();

                return;
            }
        }

        if (!myCollider2d.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            //Debug.Log(myCollider2d.IsTouchingLayers(LayerMask.GetMask("Ground")));
            myAnimator.SetLayerWeight(0, 1);
            myAnimator.SetTrigger("Iddling");
            myAnimator.SetBool("Jumping",false);
           // Debug.Log("Touching Layer");

            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
           // Debug.Log("Sppace");
            SoundManagerScript.PlaySound("Jump");
            Vector2 jumpVelocityToAdd = new Vector2(5f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
            JumpAnimation();
            return;
        }

    }

    void fire()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            projectilePosition = transform.position;
            SoundManagerScript.PlaySound("Fire");
            
            if (facingLeft)
            {
                projectilePosition += new Vector2(-0.1f, 0);
                Instantiate(projectileLeft, projectilePosition, Quaternion.identity);
            }
            else
            {
                projectilePosition += new Vector2(+0.1f, 0);
                Instantiate(projectileRight, projectilePosition, Quaternion.identity);
            }
            StartCoroutine(FireAnimation());
        }       
    }


    void Hurt(float hurtTime, int damage)
    {
        health -= damage;
        Debug.Log("HURT FUNCTION="+health);
        if (health <= 0)
            GameOverScript.gameIsOver = true;

        else
        {
            StartCoroutine(HurtBlinker(hurtTime));
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        FlyingEnemy flyingEnemy = collision.collider.GetComponent<FlyingEnemy>();
        SpecialEnemy specialEnemy = collision.collider.GetComponent<SpecialEnemy>();
        MutantFlyingEnemy mutantFlyingEnemy = collision.collider.GetComponent<MutantFlyingEnemy>();
        InstantDeath instantDeath = collision.collider.GetComponent<InstantDeath>();
        if (enemy!=null)
        {
            SoundManagerScript.PlaySound("Hurt");
            Hurt(invincibleTimeAfterHurt, 1);
            GameControl.totalLife -= 1;
            Debug.Log("Normal Enemy");
        }
        if (flyingEnemy != null)
        {
            SoundManagerScript.PlaySound("Hurt");
            Hurt(invincibleTimeAfterHurt, 2);
            GameControl.totalLife -= 2;
            Debug.Log("Flying Enemy");
        }
        if (specialEnemy!=null)
        {
            SoundManagerScript.PlaySound("Hurt");
            Hurt(invincibleTimeAfterHurt, 1);
            GameControl.totalLife -= 1;
            Debug.Log("Special Enemy");
        }
        if (mutantFlyingEnemy != null)
        {
            SoundManagerScript.PlaySound("Hurt");
            Hurt(invincibleTimeAfterHurt, 3);
            GameControl.totalLife -= 3;
            Debug.Log("Mutant Enemy");
        }
        if (instantDeath !=null)
        {
            SoundManagerScript.PlaySound("Hurt");
            Hurt(invincibleTimeAfterHurt, 3);
            GameControl.totalLife -= 3;
            Debug.Log("Instant Death");
        }

    }

    IEnumerator HurtBlinker(float hurtTime)
    {
        //Ignore Collision with Enemies
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int playerLayer = LayerMask.NameToLayer("Player");
        Physics2D.IgnoreLayerCollision(enemyLayer,playerLayer);
        //Start looping blink anime

        myAnimator.SetLayerWeight(1,1);
        //wait for invicibility to end
        yield return new WaitForSeconds(hurtTime);

        //stop blinking and re-enable collision
        Physics2D.IgnoreLayerCollision(enemyLayer,playerLayer, false);
        myAnimator.SetLayerWeight(1, 0);
    }
    
    IEnumerator FireAnimation()
    {
        Debug.Log("Fireanimation wait");
        myAnimator.SetLayerWeight(2,1);
        yield return new WaitForSeconds(0.2f);
        myAnimator.SetLayerWeight(2,0);
    }

    void JumpAnimation()
    {
        myAnimator.SetLayerWeight(0, 1);
        myAnimator.SetBool("Iddling", false);
        myAnimator.SetBool("Walking", false);
        myAnimator.SetTrigger("Jumping");
        //Debug.Log("In Air");
    }
}
