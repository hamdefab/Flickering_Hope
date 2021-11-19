using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpSpeed = 25f;

    Vector2 moveInput;
    Rigidbody2D myRididbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;

    void Start()
    {
        myRididbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Run();
        Jump();
        FlipSprite();
    }

    void OnMove(InputValue value) 
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value) 
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) 
        {
            return;
        }
        if (value.isPressed)
        {
            myRididbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRididbody.velocity.y);
        myRididbody.velocity = playerVelocity;

        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed());
    }

    void Jump()
    {
        myAnimator.SetBool("isJumping", !myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")));
    }

    void FlipSprite()
    {
        if (playerHasHorizontalSpeed()) {
            float xScale = Mathf.Abs(transform.localScale.x);
            float yScale = Mathf.Abs(transform.localScale.y);
            transform.localScale = new Vector2(Mathf.Sign(myRididbody.velocity.x) * xScale, yScale);
        }
    }

    bool playerHasHorizontalSpeed()
    {
        return Mathf.Abs(myRididbody.velocity.x) > Mathf.Epsilon;
    }

    bool playerHasVerticalSpeed()
    {
        return Mathf.Abs(myRididbody.velocity.y) > Mathf.Epsilon;
    }


}
