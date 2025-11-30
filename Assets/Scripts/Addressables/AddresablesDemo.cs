using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class AddresablesDemo : MonoBehaviour
{
    public AddresablesDemo instance { get; private set; }

    private GameObject cachedObject = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    private void Start()
    {
        InitPopUp();

        SceneManager.LoadScene(1);
    }

    public void GetAsset(string name, Action<GameObject> onComplete)
    {
        if (cachedObject != null && cachedObject.name.Equals(name))
        {
            onComplete?.Invoke(cachedObject);
            return;
        }

        StartCoroutine(IGetAsset());
        IEnumerator IGetAsset()
        {
            var asset = Addressables.LoadAssetAsync<GameObject>(name);
            yield return asset;

            if (asset.Status == AsyncOperationStatus.Succeeded)
            {
                var objInst = Addressables.InstantiateAsync(name);
                yield return objInst;

                if (objInst.Status == AsyncOperationStatus.Succeeded)
                {
                    onComplete?.Invoke(objInst.Result);
                }
            }
            else
            {
                Debug.Log("Something Went Wrong");
            }

            Addressables.Release(asset);
        }
    }

    private void InitPopUp()
    {
        GetAsset("PopUp", (obj) =>
        {
            DontDestroyOnLoad(obj);
        });
    }
}