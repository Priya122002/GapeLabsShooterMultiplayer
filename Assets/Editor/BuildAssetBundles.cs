using UnityEditor;
using UnityEngine;

public class BuildAssetBundles
{
    [MenuItem("Assets/Build Asset Bundles")]
    static void BuildAllBundles()
    {
        string path = "Assets/AssetBundles";

        if (!System.IO.Directory.Exists(path))
            System.IO.Directory.CreateDirectory(path);

        BuildPipeline.BuildAssetBundles(
            path,
            BuildAssetBundleOptions.None,
            BuildTarget.Android   // or StandaloneWindows64
        );

        Debug.Log("AssetBundles built successfully!");
    }
}
