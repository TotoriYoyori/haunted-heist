using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The player (or object) the camera will follow
    public float smoothSpeed = 0.125f; // Smoothness of camera movement
    public Vector3 offset; // Offset from the target position

    void LateUpdate()
    {
        if (target == null) return;

        // Desired position of the camera
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;
    }
}
