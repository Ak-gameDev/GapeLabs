using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleDownloader : MonoBehaviour
{
    private const string assetBundleURL = "https://drive.google.com/uc?export=download&id=1Bje-TFxZlo5IwCZh9PZU_UYyP0WqUT_5";

    private void Start()
    {
        DownloadAssetBundle((bundle) =>
        {
            GameObject player = bundle.LoadAsset<GameObject>("AssetBundle_Player");
            if (player != null)
            {
                Instantiate(player);
            }
        });
    }

    public void DownloadAssetBundle(Action<AssetBundle> onAssetBundleDownloaded)
    {
        StartCoroutine(enumerator());
        IEnumerator enumerator()
        {
            using (UnityWebRequest webRequest = UnityWebRequestAssetBundle.GetAssetBundle(assetBundleURL))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result is UnityWebRequest.Result.ProtocolError or UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
                {
                    yield break;
                }

                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(webRequest);
                onAssetBundleDownloaded?.Invoke(bundle);
            }
        }
    }
}