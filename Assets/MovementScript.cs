using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private float speed = 7;
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public KeyCode fire;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Rigidbody myRigidBody = GetComponent<Rigidbody>();
        if (Input.GetKey(up))
        {
            if (myRigidBody)
            {
                transform.forward = Vector3.forward;
                myRigidBody.velocity = new Vector3(0, 0, speed);
            }
        }
        else if (Input.GetKey(left))
        {
            if (myRigidBody)
            {
                transform.forward = Vector3.left;
                myRigidBody.velocity = new Vector3(-speed, 0, 0);
            }
        }
        else if (Input.GetKey(down))
        {
            if (myRigidBody)
            {
                transform.forward = Vector3.back;
                myRigidBody.velocity = new Vector3(0, 0, -speed);
            }
        }
        else if (Input.GetKey(right))
        {
            if (myRigidBody)
            {
                transform.forward = Vector3.right;
                myRigidBody.velocity = new Vector3(speed, 0, 0);
            }
        }
    }
}
