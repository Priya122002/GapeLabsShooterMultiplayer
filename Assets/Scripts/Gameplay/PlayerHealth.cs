using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviourPun
{
    public int maxHealth = 100;
    private int currentHealth;

    [Header("UI")]
    public Image fillHealth;
    public float smoothSpeed = 0.2f;

    private Coroutine healthRoutine;

    void Start()
    {
        currentHealth = maxHealth;

        if (photonView.IsMine)
        {
            fillHealth.fillAmount = 1f;
        }
    }

    [PunRPC]
    public void TakeDamage(int dmg)
    {
        // ✅ ONLY HIT PLAYER APPLIES DAMAGE
        if (!photonView.IsMine) return;

        currentHealth -= dmg;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // 🔥 Smooth health bar update
        if (healthRoutine != null)
            StopCoroutine(healthRoutine);

        healthRoutine = StartCoroutine(SmoothHealthUpdate());

        if (currentHealth <= 0)
        {
            Invoke(nameof(Respawn), 2f);
        }
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

    void Respawn()
    {
        currentHealth = maxHealth;
        transform.position = Vector3.zero;

        if (photonView.IsMine)
        {
            fillHealth.fillAmount = 1f;
        }
    }
}
