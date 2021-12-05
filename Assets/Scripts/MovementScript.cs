using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpSpeed = 25f;
    public GameObject spawnPoint;
    public Transform staff;
    public Transform projectilePrefab;
    [SerializeField] Vector2 knockback = new Vector2(100f, 10f);

    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthbar;
    public AudioSource ac;

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

        ac.volume = VolumeChanger.volume;
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

    void OnGUI()
    {
        if (!isAlive)
        {
            if (GUI.Button(new Rect(Screen.width * 0.5f - 100f, 400 - 20f, 400f, 100f), "Respawn"))
                Respawn();
        }
    }
        

    void Respawn()
    {
        transform.position = spawnPoint.transform.position;
        currentHealth = 100;
        healthbar.SetHealth(currentHealth);
        myAnimator.SetTrigger("Idle");
        moveInput = Vector2.zero;
        isAlive = true;
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
        if (!isAlive) { return; }
        //if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        var position = new Vector3(staff.position.x, staff.position.y, staff.position.z);

        var mousePos = Mouse.current.position.ReadValue();

        Vector3 shootPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0.0f));
        Vector3 shootDir = shootPos - position;

        var proj = Instantiate(projectilePrefab, position, Quaternion.identity);

        myAnimator.SetTrigger("Attack");
        //moveInput = Vector2.zero;

        proj.GetComponent<Projectiles>().Setup(shootDir);
    }

    void OnJump(InputValue value) 
    {
        //if (!isAlive || myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) { return; }
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
            myAnimator.SetTrigger("Hurt");
        }
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            currentHealth -= 80;
            healthbar.SetHealth(currentHealth);
            myAnimator.SetTrigger("Hurt");
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