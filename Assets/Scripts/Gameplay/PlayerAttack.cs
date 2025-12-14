using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class PlayerAttack : MonoBehaviourPun
{
    public float attackCooldown = 0.5f;
    float lastAttackTime;

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!photonView.IsMine) return;
        if (!context.performed) return;

        if (Time.time - lastAttackTime < attackCooldown)
            return;

        lastAttackTime = Time.time;

        photonView.RPC(nameof(AttackRPC), RpcTarget.All);
    }

    [PunRPC]
    void AttackRPC()
    {
        Debug.Log("Attack triggered");
        // Next step: projectile or melee
    }
}
