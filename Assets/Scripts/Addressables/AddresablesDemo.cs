using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class AddresablesDemo : MonoBehaviour
{
    private IEnumerator Start()
    {
        var init = Addressables.InitializeAsync();
        yield return init;

        var go = Addressables.LoadAssetAsync<GameObject>("Addressables_Player");
        yield return go;

        if (go.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject prefab = go.Result;

            var instHandle = Addressables.InstantiateAsync("Addressables_Player", Vector3.zero, Quaternion.identity);
            yield return instHandle;

            if (instHandle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject instance = instHandle.Result;
            }
            else
            {
                Debug.LogError("Instantiate failed: " + instHandle.OperationException);
            }

            Addressables.Release(go);
        }
    }
}