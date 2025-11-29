using Codice.Client.Common.GameUI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetBundleManagerEditor : Editor
{
    private static string assetBundlePath = "Assets/AssetBundles";

    [MenuItem("Tools/Asset Bundle/Create Asset Bundle")]
    private static void CreateAssetBundle()
    {
        if (!Directory.Exists(assetBundlePath))
        {
            Directory.CreateDirectory(assetBundlePath);
        }
        else
        {
            Directory.Delete(assetBundlePath, true);
            Directory.CreateDirectory(assetBundlePath);
        }

        BuildPipeline.BuildAssetBundles(assetBundlePath, BuildAssetBundleOptions.None, BuildTarget.Android);

        AssetDatabase.Refresh();
    }
}