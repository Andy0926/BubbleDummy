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

    public GameObject projectileLeft, projectileRight;
    Vector2 projectilePosition;

    //Projectile
    public float fireRate = 0.5f;
    float nextFire = 0;
    bool facingLeft = true;

    //Combat
    public int health = 3;
    public float invincibleTimeAfterHurt = 2;

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
            facingLeft = true;
            Debug.Log("right");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
         
            facingLeft = false;
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
        //Dont allow jump while in air

        if (myCollider2d.IsTouchingLayers(LayerMask.GetMask("Movable"))|| myCollider2d.IsTouchingLayers(LayerMask.GetMask("Trigger")))
        {
            Debug.Log("Touching Movable Block@");
            if (Input.GetKeyDown(KeyCode.Space))
            {

                Vector2 jumpVelocityToAdd = new Vector2(5f, jumpSpeed);
                myRigidBody.velocity += jumpVelocityToAdd;
                JumpAnimation();
                return;
            }
        }

        if (!myCollider2d.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Debug.Log(myCollider2d.IsTouchingLayers(LayerMask.GetMask("Ground")));
            myAnimator.SetLayerWeight(0, 1);
            myAnimator.SetTrigger("Iddling");
            myAnimator.SetBool("Jumping",false);
            Debug.Log("Touching Layer");

            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {

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
            if (facingLeft)
            {
                projectilePosition += new Vector2(+0.1f, 0);
                Instantiate(projectileRight, projectilePosition, Quaternion.identity);
                //Debug.Log(projectilePosition);
            }
            else
            {
                projectilePosition += new Vector2(-0.1f, 0);
                Instantiate(projectileLeft, projectilePosition, Quaternion.identity);
                //Debug.Log(projectilePosition);
            }
        }
    }
    void Hurt(float hurtTime, int damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
            Application.LoadLevel(Application.loadedLevel);

        else
        {
            StartCoroutine(HurtBlinker(hurtTime));
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        FlyingEnemy flyingEnemy = collision.collider.GetComponent<FlyingEnemy>();
        if (enemy!=null)
        {
            Hurt(invincibleTimeAfterHurt, 1);
            GameControl.totalLife -= 1;
        }
        if (flyingEnemy != null)
        {
            Hurt(invincibleTimeAfterHurt, 2);
            GameControl.totalLife -= 2;
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

    void JumpAnimation()
    {
        myAnimator.SetLayerWeight(0, 1);
        myAnimator.SetBool("Iddling", false);
        myAnimator.SetBool("Walking", false);
        myAnimator.SetTrigger("Jumping");
        //Debug.Log("In Air");
    }
}
