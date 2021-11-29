using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpSpeed = 25f;
    [SerializeField] Vector2 knockback = new Vector2(100f, 10f);

    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthbar;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;

    bool isAlive;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();

        healthbar.SetMaxHealth(maxHealth);
        isAlive = true;
    }

    void Update()
    {
        if (!isAlive) { return; }
        Run();
        Jump();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            moveInput = Vector2.zero;
        }
        else { moveInput = value.Get<Vector2>(); }
    }

    void OnAttack(InputValue value)
    {
        if (!isAlive || myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) { return; }
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        myAnimator.SetTrigger("Attack");
        moveInput = Vector2.zero;

    }

    void OnJump(InputValue value) 
    {
        if (!isAlive || myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) { return; }
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (value.isPressed)
        {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

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
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x) * xScale, yScale);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Enemies")) 
        {
            currentHealth -= 20;
            healthbar.SetHealth(currentHealth);
        }
        if (currentHealth <= 0 && isAlive)
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
        }
    }

    bool playerHasHorizontalSpeed()
    {
        return Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
    }

    bool playerHasVerticalSpeed()
    {
        return Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
    }


}
