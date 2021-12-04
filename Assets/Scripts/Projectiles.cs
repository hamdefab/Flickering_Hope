using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public Rigidbody2D rb;
    public float shootSpeed = 5f;
    private Vector3 shootDir;
    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
    }

    public void Update()
    {
        rb.velocity = new Vector2(shootDir.x, shootDir.y).normalized * shootSpeed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.gameObject);
        //if (other.collider.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        //{
            
        //}
    }

}