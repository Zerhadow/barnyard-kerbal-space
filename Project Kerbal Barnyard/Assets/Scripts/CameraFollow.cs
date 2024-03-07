using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the target (ship in this case)
    public float smoothSpeed = 0.125f;   // The speed at which the camera will follow the target

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
