using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleLoader : MonoBehaviour
{
    public string bundleUrl;
    public string prefabName = "BundleCube";
    public Transform[] spawnPoints;

    IEnumerator Start()
    {
        UnityWebRequest request = UnityWebRequest.Get(bundleUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            ErrorPopup.Instance.Show(
                "AssetBundle download failed",
                () => StartCoroutine(Start())
            );
            yield break;
        }

        AssetBundle bundle =
            AssetBundle.LoadFromMemory(request.downloadHandler.data);

        if (bundle == null)
        {
            ErrorPopup.Instance.Show(
                "Failed to load AssetBundle",
                () => StartCoroutine(Start())
            );
            yield break;
        }

        GameObject prefab = bundle.LoadAsset<GameObject>(prefabName);

        if (prefab == null)
        {
            ErrorPopup.Instance.Show(
                "Prefab not found in AssetBundle",
                () => StartCoroutine(Start())
            );
            yield break;
        }

        foreach (Transform point in spawnPoints)
        {
            GameObject obj = Instantiate(prefab, point);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
        }
    }
}
