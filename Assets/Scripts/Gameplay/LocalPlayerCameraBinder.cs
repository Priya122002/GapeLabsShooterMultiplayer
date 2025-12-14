using Photon.Pun;
using UnityEngine;

public class LocalPlayerCameraBinder : MonoBehaviourPun
{
    void Start()
    {
        if (!photonView.IsMine) return;

        // Camera follow
        Camera.main
            .GetComponent<CameraFollow>()
            .SetTarget(transform);

        UIManager.Instance.RegisterLocalPlayer(
            GetComponent<PlayerAttack>()
        );
    }
}
