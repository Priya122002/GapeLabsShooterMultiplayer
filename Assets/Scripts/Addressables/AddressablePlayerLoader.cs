using Photon.Pun;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UI;

public class AddressablePlayerLoader : MonoBehaviourPun
{
    public AssetReference modelReference;

    void Start()
    {
        if (!photonView.IsMine) return;
        LoadModel();
    }

    void LoadModel()
    {
        Addressables.LoadAssetAsync<GameObject>(modelReference)
            .Completed += handle =>
            {
                if (handle.Status != AsyncOperationStatus.Succeeded)
                {
                    ErrorPopup.Instance.ShowRetry(LoadModel);
                    return;
                }

                Instantiate(handle.Result, transform);
                Addressables.Release(handle);
            };
    }
}
