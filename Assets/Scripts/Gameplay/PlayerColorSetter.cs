using Photon.Pun;
using UnityEngine;

public class PlayerColorSetter : MonoBehaviourPun
{
    [Header("Materials")]
    public Material localPlayerMat;   
    public Material remotePlayerMat;  

    [Header("Renderers")]
    public Renderer[] renderers; 

    void Start()
    {
        ApplyColor();
    }

    void ApplyColor()
    {
        Material matToApply =
            photonView.IsMine ? localPlayerMat : remotePlayerMat;

        foreach (Renderer r in renderers)
        {
            r.material = matToApply;
        }
    }
}
