using Photon.Pun;
using UnityEngine;

public class LocalPlayerCameraBinder : MonoBehaviourPun
{
    void Start()
    {
        if (!photonView.IsMine) return;

        Camera.main
            .GetComponent<CameraFollow>()
            .SetTarget(transform);
    }
}
