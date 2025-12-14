using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner Instance;
    public Transform[] spawnPoints;

    bool spawned;

    void Awake()
    {
        Instance = this;
    }

    public void Spawn()
    {
        Debug.Log("Spawn called");

        if (spawned) return;

        Debug.Log("Spawning player");

        spawned = true;

        Transform spawn =
            spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber % spawnPoints.Length];

        PhotonNetwork.Instantiate("Player", spawn.position, Quaternion.identity);
    }

}
