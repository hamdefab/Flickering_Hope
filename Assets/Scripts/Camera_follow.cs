using UnityEngine;

public class Camera_follow : MonoBehaviour
{
    public Transform character;
    public Transform background_pic;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void FixedUpdate()
    {
        Vector3 desiredPosition = character.position + offset;
        transform.position = desiredPosition;
        background_pic.transform.position = character.position;
    }
}
