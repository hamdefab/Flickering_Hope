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
        // https://www.youtube.com/watch?v=Nke5JKPiQTw&t=303s
        // how to rotate bullet
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(shootDir));
    }

    public float GetAngleFromVectorFloat(Vector3 dir) {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
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