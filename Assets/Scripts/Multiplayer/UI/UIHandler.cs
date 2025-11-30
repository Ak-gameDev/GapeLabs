using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private GameObject bgPanel;
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private GameObject playerControlsPanel;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject connectButton;
    [SerializeField] private GameObject joinButton;

    internal void ShowLoading(bool show)
    {
        loadingScreen.SetActive(show);
    }

    internal void OnServerConnected()
    {
        connectButton.SetActive(false);
        joinButton.SetActive(true);
    }

    internal void OnRoomJoined()
    {
        loadingScreen.SetActive(false);
        menuScreen.SetActive(false);
        bgPanel.SetActive(false);
        playerControlsPanel.SetActive(true);
    }
}