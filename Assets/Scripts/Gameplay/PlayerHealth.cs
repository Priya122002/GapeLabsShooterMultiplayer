using Photon.Pun;
using UnityEngine;

public class PlayerHealth : MonoBehaviourPun
{
    public int maxHealth = 100;
    int health;

    void Start()
    {
        health = maxHealth;
    }
    [PunRPC]
    public void TakeDamage(int dmg)
    {
        health -= dmg;

        if (health <= 0 && photonView.IsMine)
        {
            Invoke(nameof(Respawn), 2f);
        }
    }


    void Respawn()
    {
        health = maxHealth;
        transform.position = Vector3.zero;
    }
}
