using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviourPun
{
    [Header("Health")]
    public int maxHealth = 100;          // 100 HP
    public int currentHealth;

    [Header("UI")]
    public Image fillHealth;
    public float smoothSpeed = 0.2f;
    float lastHitTime;
    float hitCooldown = 0.15f;
    Coroutine healthRoutine;

    // 🔥 STORE DEATH POSITION
    Vector3 deathPosition;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUIInstant();
    }

    [PunRPC]
    public void TakeDamage(int dmg)
    {
        if (!photonView.IsMine) return;

        if (Time.time - lastHitTime < hitCooldown)
        {
            Debug.Log("❌ Duplicate hit ignored");
            return;
        }

        lastHitTime = Time.time;

        currentHealth -= dmg;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"✅ Damage applied: {dmg}, Health now: {currentHealth}");

        photonView.RPC(
            nameof(SyncHealth),
            RpcTarget.All,
            currentHealth
        );

        if (currentHealth <= 0)
        {
            deathPosition = transform.position;
            Invoke(nameof(Respawn), 2f);
        }
    }


    // 🔥 EVERY CLIENT UPDATES HEALTH BAR
    [PunRPC]
    void SyncHealth(int newHealth)
    {
        currentHealth = newHealth;

        if (healthRoutine != null)
            StopCoroutine(healthRoutine);

        healthRoutine = StartCoroutine(SmoothHealthUpdate());
    }

    IEnumerator SmoothHealthUpdate()
    {
        float startFill = fillHealth.fillAmount;
        float targetFill = (float)currentHealth / maxHealth;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / smoothSpeed;
            fillHealth.fillAmount = Mathf.Lerp(startFill, targetFill, t);
            yield return null;
        }

        fillHealth.fillAmount = targetFill;
    }

    void UpdateHealthUIInstant()
    {
        fillHealth.fillAmount = 1f;
    }

    // 🔥 RESPAWN AT SAME PLACE
    void Respawn()
    {
        currentHealth = maxHealth;

        photonView.RPC(
            nameof(SyncHealth),
            RpcTarget.All,
            currentHealth
        );

        // ✅ RESPAWN EXACTLY WHERE PLAYER DIED
        transform.position = deathPosition;
    }
}
