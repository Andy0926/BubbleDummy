using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Collider2D myCollider2d;
    //CapsuleCollider2D capsuleCollider2D;
    //BoxCollider2D boxCollider2D;
    [SerializeField] float runSpeed = 1.5f;
    [SerializeField] float jumpSpeed = 10.0f;
    [SerializeField] private Transform[] groundPoints;
    [SerializeField ]private float groundRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask platformLayerMask;

    private bool isGrounded;
    void Start()
    {
        myRigidBody = transform.GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        //capsuleCollider2D = transform.GetComponent<CapsuleCollider2D>();
        //boxCollider2D = transform.GetComponent<BoxCollider2D>();
        myCollider2d = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //isGrounded = IsGrounded();
        Jump();
        Walk();
        FlipSprite();
        
        //HandleLayers();
    }

    private void Walk()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); //value is between -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool walking = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Walking", walking);
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
  
}
