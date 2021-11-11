using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpSpeed = 20f;

    Vector2 moveInput;
    Rigidbody2D myRididbody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;

    void Start()
    {
        myRididbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
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
        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) 
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
        myAnimator.SetBool("isJumping", !myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")));
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
