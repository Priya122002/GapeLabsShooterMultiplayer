using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleLoader : MonoBehaviour
{
    [Header("GitHub RAW URL")]
    public string bundleUrl;

    [Header("Prefab Name")]
    public string prefabName = "BundleCube";

    IEnumerator Start()
    {
        Debug.Log("Downloading AssetBundle...");

        UnityWebRequest request = UnityWebRequest.Get(bundleUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Download failed: " + request.error);
            yield break;
        }

        byte[] bundleData = request.downloadHandler.data;

        // 🔥 Load from BYTES
        AssetBundle bundle = AssetBundle.LoadFromMemory(bundleData);

        if (bundle == null)
        {
            Debug.LogError("AssetBundle load failed");
            yield break;
        }

        Debug.Log("AssetBundle loaded successfully!");

        GameObject prefab = bundle.LoadAsset<GameObject>(prefabName);

        if (prefab == null)
        {
            Debug.LogError("Prefab not found in bundle");
            yield break;
        }

        Instantiate(prefab, Vector3.zero, Quaternion.identity);

        Debug.Log("Prefab instantiated from AssetBundle!");
    }
}
