using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public float shootSpeed = 50f;
    private Vector3 shootDir;
    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        Debug.Log(shootDir);
    }

    public void Update()
    {
        transform.position += shootDir * shootSpeed * Time.deltaTime;
    }
}