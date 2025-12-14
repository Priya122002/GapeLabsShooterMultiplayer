using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        if (PhotonNetwork.InRoom)
        {
            PlayerSpawner.Instance.Spawn();
        }
    }
}
