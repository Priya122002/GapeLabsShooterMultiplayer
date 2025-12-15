using Photon.Pun;
using UnityEngine;

public class PlayerBillboard : MonoBehaviourPun
{
    Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (cam == null) return;

        // 🔥 Correct direction: object → camera
        transform.rotation = Quaternion.LookRotation(
            cam.position - transform.position
        );
    }
}
