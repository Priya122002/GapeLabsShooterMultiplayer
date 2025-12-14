using Photon.Pun;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 12f;
    public float lifeTime = 2f;
    public int damage = 10;

    private float timer;
    private Transform visualChild;

    void Awake()
    {
        visualChild = transform.GetChild(0); // visual mesh
    }

    void OnEnable()
    {
        timer = lifeTime;
        visualChild.localRotation = Quaternion.identity;
    }

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
            ReturnToPool();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerHealth health = other.GetComponent<PlayerHealth>();
        if (health != null)
        {
            // ✅ Send to all
            health.photonView.RPC(
                nameof(PlayerHealth.TakeDamage),
                RpcTarget.All,
                damage
            );
        }

        ReturnToPool();
    }



    void ReturnToPool()
    {
        ObjectPooler.Instance.ReturnToPool("Projectile", gameObject);
    }
}
