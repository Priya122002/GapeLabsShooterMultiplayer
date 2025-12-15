using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerNameTag : MonoBehaviourPun
{
    [SerializeField] private TextMeshProUGUI nameText;

    Transform cam;

    void Start()
    {
        cam = Camera.main.transform;

        nameText.text = photonView.Owner.NickName;

    }

   /* void LateUpdate()
    {
        if (cam == null) return;

        // 🔥 Direction from name tag to camera (ignore Y)
        Vector3 lookDir = cam.position - transform.position;
        lookDir.y = 0f;

        if (lookDir.sqrMagnitude < 0.001f) return;

        // 🔥 Rotate ONLY on Y axis
        transform.rotation = Quaternion.LookRotation(lookDir);
    }*/
}
