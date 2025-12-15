using UnityEngine;
using Photon.Pun;

public class PlayerAttack : MonoBehaviourPun
{
    public Transform gunPoint;
    public float attackCooldown = 0.001f;

    float lastFireTime;

    public void Fire()
    {
        if (!photonView.IsMine) return;
        if (Time.time - lastFireTime < attackCooldown) return;

        lastFireTime = Time.time;
        photonView.RPC(
            nameof(RPC_Fire),
            RpcTarget.All,
            gunPoint.position,
            gunPoint.rotation
        );
    }

    [PunRPC]
    void RPC_Fire(Vector3 pos, Quaternion rot)
    {
        if (!ObjectPooler.Instance.IsReady) return;

        GameObject bullet = ObjectPooler.Instance.Spawn(
            "Projectile",
            pos,
            rot
        );

        bullet.GetComponent<Projectile>().RotateVisual90();
    }

}
