using System;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    public static PopUp instance { get; private set; }

    [SerializeField] private Button retryButton;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    private void Start()
    {
        HidePopUP();
    }

    public void ShowPopUp(Action retryAction = null)
    {
        retryButton.onClick.RemoveAllListeners();
        retryButton.onClick.AddListener(() =>
        {
            retryAction?.Invoke();
            HidePopUP();
        });

        gameObject.SetActive(true);
    }

    public void HidePopUP()
    {
        gameObject.SetActive(false);
    }
}