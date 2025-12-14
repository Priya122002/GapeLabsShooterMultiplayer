using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform target;

    [Header("Camera Offset")]
    public Vector3 offset = new Vector3(3f, 5f, -3.5f);


    [Header("Follow Settings")]
    public float followSpeed = 8f;
    public float rotationSpeed = 10f;

    public void SetTarget(Transform t)
    {
        target = t;
    }

    void LateUpdate()
    {
        if (!target) return;

        // Position
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            followSpeed * Time.deltaTime
        );

        // Rotation (look at player)
        Quaternion lookRotation = Quaternion.LookRotation(
            target.position - transform.position
        );

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            lookRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}
