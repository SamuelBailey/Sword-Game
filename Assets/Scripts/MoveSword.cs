using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSword : MonoBehaviour
{
    public Transform playerPosition;
    public float swordSpeed;
    Rigidbody swordRigidbody;
    public GameObject player;
    Vector3 swordOrigin;
    public float swordDistance;
    Vector3 targetAngle;
    public float maxSwordAngle;
    public int smoothing;
    public float sensitivity;

    private void Start()
    {
        swordRigidbody = gameObject.GetComponent<Rigidbody>();
        swordRigidbody.velocity = new Vector3(0, 0, 0);
        //swordOrigin = new Vector3(0, 0, swordDistance);
        swordOrigin = swordRigidbody.transform.position - player.transform.position;

        targetAngle = new Vector3(0.0f, 0.0f);
    }

    Vector3 lastPosition = new Vector3(0, 0, 0);

    private void FixedUpdate()
    {
        targetAngle.x += Mathf.Clamp(Input.GetAxis("Mouse X"), -100, 100) * sensitivity;
        targetAngle.y += Mathf.Clamp(Input.GetAxis("Mouse Y"), -100, 100) * sensitivity;
        if (Vector3.Magnitude(targetAngle) > maxSwordAngle)
            targetAngle = targetAngle.normalized * maxSwordAngle;

        // find sword current angle
        float xAngle = Vector3.Angle(
            new Vector3(swordOrigin.x, 0.0f, swordOrigin.z), 
            new Vector3(swordRigidbody.transform.position.x, 0.0f, swordRigidbody.transform.position.z)
        );
        float yAngle = Vector3.Angle(
            new Vector3(0.0f, swordOrigin.y, swordOrigin.z), 
            new Vector3(0.0f, swordRigidbody.transform.position.y, swordRigidbody.transform.position.z)
        );
        Vector3 swordAngle = new Vector3(xAngle, yAngle);
        // find the amount and in which direction to move the sword
        Vector3 directionAngle = Vector3.Lerp(swordAngle, targetAngle, 0.5f);

        lastPosition = (lastPosition * smoothing + targetAngle) / (smoothing + 1);


        Vector3 direction = new Vector3();
        gameObject.transform.rotation = player.transform.rotation;
        gameObject.transform.position = player.transform.position;
        gameObject.transform.localPosition = new Vector3(0, 0, 1);
        gameObject.transform.RotateAround(player.transform.position, Vector3.up, lastPosition.x);
        gameObject.transform.RotateAround(player.transform.position, Vector3.left, lastPosition.y);
        
        Debug.Log(targetAngle);
    }

    void RotateRigidBodyAboutPoint(Rigidbody rb, Vector3 origin, Vector3 axis, float angle)
    {
        Quaternion q = Quaternion.AngleAxis(angle, axis);
        rb.MovePosition(q * (rb.transform.position - origin) + origin);
        rb.MoveRotation(rb.transform.rotation * q);
    }
}
