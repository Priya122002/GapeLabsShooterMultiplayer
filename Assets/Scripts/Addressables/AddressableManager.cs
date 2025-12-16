using UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableManager : MonoBehaviour
{
    public static AddressableManager Instance;

    [Header("Arena (Addressable)")]
    public AssetReference floorReference;
    public Transform environmentParent;

    public static bool ArenaLoaded { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        LoadArena();
    }


    void LoadArena()
    {
        Addressables.LoadAssetAsync<GameObject>(floorReference)
            .Completed += OnArenaLoaded;
    }

    void OnArenaLoaded(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate(handle.Result, environmentParent);
            ArenaLoaded = true;

            GameManager.Instance.TrySpawnPlayer();
        }
        else
        {
            ErrorPopup.Instance.Show("Failed to load arena",LoadArena);
        }
    }
}
