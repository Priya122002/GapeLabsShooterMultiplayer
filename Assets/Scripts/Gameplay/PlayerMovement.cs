using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun
{
    public float moveSpeed = 5f;

    [Header("Rotation")]
    public float rotationSpeed = 6f;          // lower = smoother
    public float inputDeadZone = 0.15f;        // joystick jitter filter

    VirtualJoystick joystick;
    Transform cam;

    Vector3 smoothMoveDir;

    void Start()
    {
        if (!photonView.IsMine)
        {
            enabled = false;
            return;
        }

        joystick = FindObjectOfType<VirtualJoystick>();
        cam = Camera.main.transform;
    }

    void Update()
    {
        if (joystick == null) return;

        Vector2 input = joystick.InputVector;

        // 🔥 Dead zone (important for smoothness)
        if (input.magnitude < inputDeadZone)
            return;

        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 rawMoveDir =
            camForward * input.y +
            camRight * input.x;

        // 🔥 Smooth direction change
        smoothMoveDir = Vector3.Lerp(
            smoothMoveDir,
            rawMoveDir.normalized,
            Time.deltaTime * 8f
        );

        // Move
        transform.Translate(
            smoothMoveDir * moveSpeed * Time.deltaTime,
            Space.World
        );

        // 🔥 Smooth rotation
        if (smoothMoveDir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(smoothMoveDir);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
}
