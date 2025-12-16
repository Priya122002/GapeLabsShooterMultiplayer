using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviourPun
{
    public int maxHealth = 100;
    public int currentHealth;

    public Image fillHealth;

    PlayerMovement movement;
    PlayerAttack attack;
    PlayerDissolveController dissolve;

    bool isDead;
    Vector3 deathPosition;

    void Start()
    {
        currentHealth = maxHealth;
        movement = GetComponent<PlayerMovement>();
        attack = GetComponent<PlayerAttack>();
        dissolve = GetComponent<PlayerDissolveController>();

        UpdateHealthUI();
    }

    // 🔥 CALLED FROM PROJECTILE (RPC → All)
    [PunRPC]
    public void TakeDamage(int dmg)
    {
        if (!photonView.IsMine) return;
        if (isDead) return;

        currentHealth -= dmg;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        photonView.RPC(nameof(RPC_SyncHealth), RpcTarget.All, currentHealth);

        if (currentHealth <= 0)
        {
            isDead = true;
            deathPosition = transform.position;

            StartCoroutine(DeathRoutine());
        }
    }

    IEnumerator DeathRoutine()
    {
        // 🔒 disable gameplay locally
        movement.enabled = false;
        attack.enabled = false;
        SoundManager.Instance.Play("die");
        // 🔥 dissolve burst for everyone
        photonView.RPC(nameof(RPC_DissolveOut), RpcTarget.All);

        yield return new WaitForSeconds(2f);

        Respawn();
    }

    void Respawn()
    {
        currentHealth = maxHealth;
        transform.position = deathPosition;

        photonView.RPC(nameof(RPC_SyncHealth), RpcTarget.All, currentHealth);
        photonView.RPC(nameof(RPC_DissolveIn), RpcTarget.All);

        movement.enabled = true;
        attack.enabled = true;

        isDead = false;
    }

    // ================= RPCs =================

    [PunRPC]
    void RPC_SyncHealth(int hp)
    {
        currentHealth = hp;
        UpdateHealthUI();
    }

    [PunRPC]
    void RPC_DissolveOut()
    {
        if (dissolve != null)
            dissolve.PlayDissolveOutBurst();
    }

    [PunRPC]
    void RPC_DissolveIn()
    {
        if (dissolve != null)
            dissolve.PlayDissolveInBurst();
    }

    void UpdateHealthUI()
    {
        if (fillHealth)
            fillHealth.fillAmount = (float)currentHealth / maxHealth;
    }
}
