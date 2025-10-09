using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;      // The character to follow
    public float smoothSpeed = 0.125f;  // How smooth the camera follows
    public Vector3 offset;        // Offset distance between camera and player

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Optional: keep camera rotation fixed
        transform.LookAt(target);
    }
}
