using Photon.Pun;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform visual;
    public float speed = 12f;
    public float lifeTime = 2f;
    public int damage = 5;
    float timer;
    bool hasHit;
    Collider col;

    void Awake()
    {
        col = GetComponent<Collider>();
    }

    void OnEnable()
    {
        timer = lifeTime;
        hasHit = false;

        if (col != null)
            col.enabled = true;

        if (visual != null)
            visual.localRotation = Quaternion.identity;
    }

    public void RotateVisual90()
    {
        if (visual != null)
            visual.localRotation = Quaternion.Euler(90f, 0f, 0f);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        timer -= Time.deltaTime;
        if (timer <= 0f)
            ReturnToPool();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (hasHit) return;

        hasHit = true;

        if (col != null)
            col.enabled = false;

        if (PhotonNetwork.IsMasterClient)
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                Debug.Log(
                    $"[SEND DAMAGE] Damage:{damage} → PlayerView:{health.photonView.ViewID}"
                );

                health.photonView.RPC(
     nameof(PlayerHealth.TakeDamage),
     RpcTarget.All,
     damage
 );

            }
        }

        ReturnToPool();
    }


    void ReturnToPool()
    {
        ObjectPooler.Instance.ReturnToPool("Projectile", gameObject);
    }
}
