using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = -1f;
    Rigidbody2D myRigidbody;

    public int currentHealth = 100;
    public HealthBar healthbar;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed, 0f);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Weapon") && other.collider.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            currentHealth -= 20;
            healthbar.SetHealth(currentHealth);
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(moveSpeed)), 1f);
    }
}
