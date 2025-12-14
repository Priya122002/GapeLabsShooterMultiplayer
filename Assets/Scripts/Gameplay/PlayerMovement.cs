using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    VirtualJoystick joystick;
    Transform cam;

    void Start()
    {
        if (!photonView.IsMine)
        {
            enabled = false; // 🔥 optimization
            return;
        }

        joystick = FindObjectOfType<VirtualJoystick>();
        cam = Camera.main.transform;
    }

    void Update()
    {
        if (joystick == null) return;

        Vector2 input = joystick.InputVector;
        if (input.sqrMagnitude < 0.01f) return;

        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir =
            camForward * input.y +
            camRight * input.x;

        transform.Translate(
            moveDir * moveSpeed * Time.deltaTime,
            Space.World
        );

        Quaternion targetRotation = Quaternion.LookRotation(moveDir);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}
