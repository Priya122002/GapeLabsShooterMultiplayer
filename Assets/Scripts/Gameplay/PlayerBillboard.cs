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

        // Direction from billboard to camera
        Vector3 lookDir = cam.position - transform.position;

        // 🔥 Ignore vertical difference (NO X rotation)
        lookDir.y = 0f;

        if (lookDir.sqrMagnitude < 0.001f)
            return;

        // 🔥 Rotate ONLY around Y axis
        transform.rotation = Quaternion.LookRotation(lookDir);
    }
}
