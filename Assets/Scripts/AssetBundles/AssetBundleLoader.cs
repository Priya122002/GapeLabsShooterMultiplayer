using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class AssetBundleLoader : MonoBehaviour
{
    public string bundleUrl;

    IEnumerator Start()
    {
        UnityWebRequest req = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            UI.ErrorPopup.Instance.ShowRetry(() => StartCoroutine(Start()));
            yield break;
        }

        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(req);
        Instantiate(bundle.LoadAsset<GameObject>("BundlePrefab"));
    }
}
