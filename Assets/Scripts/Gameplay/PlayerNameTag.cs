using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerNameTag : MonoBehaviourPun
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject localPointer;

    void Start()
    {
        if (photonView.IsMine)
        {
            nameText.text = "You";
            nameText.color = Color.green;

            if (localPointer != null)
                localPointer.SetActive(true);
        }
        else
        {
            nameText.text = photonView.Owner.NickName;
            nameText.color = Color.white;

            if (localPointer != null)
                localPointer.SetActive(false);
        }
    }
}
