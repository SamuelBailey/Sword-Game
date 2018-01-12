using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSword : MonoBehaviour
{
    public Transform playerPosition;
    public float swordSpeed;
    Rigidbody swordRigidbody;
    Vector3 playerOffset;

    private void Start()
    {
        swordRigidbody = gameObject.GetComponent<Rigidbody>();
        playerOffset = gameObject.transform.position - playerPosition.position;
    }

    private void FixedUpdate()
    {
        Quaternion.Euler(Input.GetAxisRaw("Mouse Y"), Input.GetAxisRaw("Mouse X"), 0);
        transform.RotateAround(playerPosition.position, Vector3.up, Input.GetAxisRaw("Mouse X") * swordSpeed);
        transform.RotateAround(playerPosition.position, Vector3.right, Input.GetAxisRaw("Mouse Y") * swordSpeed * -1);
        //Debug.Log(Vector3.Angle())
    }
}
