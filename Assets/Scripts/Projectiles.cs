using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public Rigidbody2D rb;
    public float shootSpeed = 10f;
    private Vector3 shootDir;
    private float tm;


    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
    }

    public void Update()
    {
        tm += Time.deltaTime;

        if (tm > 3.0f) { Destroy(this.gameObject); }

        rb.velocity = new Vector2(shootDir.x, shootDir.y).normalized * shootSpeed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.gameObject);
    }

}