using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerNameTag : MonoBehaviourPun
{
    [SerializeField] private TextMeshProUGUI nameText;


    void Start()
    {
        nameText.text = photonView.Owner.NickName;

    }

  
}
