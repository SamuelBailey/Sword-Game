using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float acceleration;
    Rigidbody playerRigidbody;

    private void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //Lateral movement
        Vector3 targetVelocity = new Vector3();
        targetVelocity.x = Input.GetAxisRaw("Horizontal");  // sideways movement
        targetVelocity.z = Input.GetAxisRaw("Vertical");    // forwards/backwards movement
        targetVelocity = targetVelocity.normalized;
        targetVelocity *= moveSpeed;
        playerRigidbody.AddForce(findMovementForce(playerRigidbody.velocity, targetVelocity, acceleration));
    }

    Vector3 findMovementForce(Vector3 currentVelocity, Vector3 targetVelocity, float acceleration)
    {
        currentVelocity.y = 0;
        return (targetVelocity - currentVelocity) * acceleration;
    }
}
