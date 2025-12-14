using Photon.Pun;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 12f;
    public float lifeTime = 2f;
    public int damage = 20;

    float timer;
    Transform visualChild;

    void Awake()
    {
        // cache the visual child
        visualChild = transform.GetChild(0);
    }

    void OnEnable()
    {
        timer = lifeTime;

        // reset visual rotation first
        visualChild.localRotation = Quaternion.identity;
    }

    // 🔥 CALL THIS WHEN FIRING
    public void RotateVisual90()
    {
        visualChild.localRotation = Quaternion.Euler(90f, 0f, 0f);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            ObjectPooler.Instance.ReturnToPool("Projectile", gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerHealth health = other.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.photonView.RPC(
                nameof(PlayerHealth.TakeDamage),
                RpcTarget.All,
                damage
            );
        }

        ObjectPooler.Instance.ReturnToPool("Projectile", gameObject);
    }
}
