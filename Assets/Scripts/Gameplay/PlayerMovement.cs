using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun
{
    public float moveSpeed = 5f;

    Vector2 moveInput;
    VirtualJoystick joystick;

    void Start()
    {
        if (!photonView.IsMine) return;

        joystick = FindObjectOfType<VirtualJoystick>();
    }

    void Update()
    {
        if (!photonView.IsMine) return;

        if (joystick != null)
            moveInput = joystick.inputVector;

        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
    }
}
