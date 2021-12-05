using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = -1f;
    Rigidbody2D myRigidbody;

    public int currentHealth = 100;
    public int damage = 20;
    public HealthBar healthbar;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed, 0f);
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            currentHealth -= damage;
            healthbar.SetHealth(currentHealth);
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Weapon"))
        {
            moveSpeed = -moveSpeed;
            FlipEnemyFacing();
        }
        
    }

    void FlipEnemyFacing()
    {
        float xScale = Mathf.Abs(transform.localScale.x);
        float yScale = Mathf.Abs(transform.localScale.y);
        transform.localScale = new Vector2(-(Mathf.Sign(moveSpeed)) * xScale, yScale);
    }
}
