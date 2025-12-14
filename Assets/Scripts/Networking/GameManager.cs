using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        TrySpawnPlayer();
    }

    public override void OnJoinedRoom()
    {
        TrySpawnPlayer();
    }

    public void TrySpawnPlayer()
    {
        if (!PhotonNetwork.InRoom) return;
        if (!AddressableManager.ArenaLoaded) return;

        PlayerSpawner.Instance.Spawn();
    }

}
