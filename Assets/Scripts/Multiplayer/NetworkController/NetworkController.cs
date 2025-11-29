using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using ExitGames.Client.Photon;
using Player = Photon.Realtime.Player;


public class NetworkController : MonoBehaviourPunCallbacks, IInRoomCallbacks, IOnEventCallback
{
    [Header("SCRIPTS")]
    //[SerializeField] GameManager gamemanager;


    [SerializeField] TypedLobby FreeToPlayLobby = new("FreeLobby", LobbyType.Default);

    [SerializeField] private int maxPlayerCount = 4;

    [Header("Lobby")]
    //[SerializeField] internal List<LobbyPlayer> lobbyPlayerPrefabList = new();

    [Header("ROOM LISTING")]
    private List<RoomInfo> currentLobbyRoomList = new();

    [SerializeField] int sceneLoadedPlayersCount;
    new private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    new private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    private void SetPlayerCustomPropsOnLobbyJoining()
    {
        Hashtable prop = new();
        //prop.Add(NetworkPropertyKeys._PlayerReadyInLobby, 0);
        prop.Add(NetworkPropertyKeys._PlayerSceneLoadingStatus, 0);

        sceneLoadedPlayersCount = 0;

        //gamemanager.localPlayer.NickName = gamemanager.GetPlayerName();
        //gamemanager.localPlayer.SetCustomProperties(prop);
    }
    internal void ConnectServer_JoinLobby()
    {
        StartCoroutine(IConnectServer());
    }
    private IEnumerator IConnectServer()
    {
        //HelperScript.LoadingPanel(true, "Connecting to Server");

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
            yield return new WaitForSeconds(0.5f);
        }

        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        //HelperScript.LoadingPanel(false);
        //gamemanager.localPlayer = PhotonNetwork.LocalPlayer;
        //lobbyPlayerPrefabList.Clear();

        JoinLobby();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        //gamemanager.isPlayingGame = false;
        //CustomActions._OnDisposeObjectsOnLeavingNetwork?.Invoke();
    }
    private void JoinLobby()
    {
        //HelperScript.LoadingPanel(true, "Joining Lobby");

        PhotonNetwork.JoinLobby(FreeToPlayLobby);
    }
    public override void OnJoinedLobby()
    {
        //HelperScript.LoadingPanel(false);

        SetPlayerCustomPropsOnLobbyJoining();
    }
    internal void JoinCreatedRoom()
    {
        //HelperScript.LoadingPanel(true, "Joining Room");

        RoomOptions ops = new()
        {
            MaxPlayers = maxPlayerCount,
            IsVisible = true,
            IsOpen = true
        };

        PhotonNetwork.JoinOrCreateRoom("Room_Name", ops, FreeToPlayLobby);
    }
    public override void OnJoinedRoom() //LOCAL CALLBACK
    {
        PlayerCountCheck();

        //HelperScript.LoadingPanel(true, "Getting Room Data...");

        //gamemanager.uiController.LoadLobbyRoom();

        List<Photon.Realtime.Player> plList = new();

        foreach (var info in PhotonNetwork.PlayerList)
        {
            plList.Add(info);
        }

        plList = plList.OrderBy(x => x.ActorNumber).ToList();

        foreach (var p in plList)
        {
            SpawnLobbyEntity(p);
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //HelperScript.LoadingPanel(false);
        //HelperScript.ShowNetworkError(returnCode);
    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer) //CALLBACK TO OTHERS
    {
        SpawnLobbyEntity(newPlayer);

        PlayerCountCheck();
    }
    private void SpawnLobbyEntity(Photon.Realtime.Player player)
    {
        //int index = lobbyPlayerPrefabList.FindIndex(x => x.thisPlayer == player);

        //if (index != -1) //If Already Exist
        //{
        //    var item = lobbyPlayerPrefabList[index];
        //    item.DestroyPrefab();
        //    lobbyPlayerPrefabList.RemoveAt(index);
        //}

        //var lobbyP = gamemanager.uiController.SpawnLobbyEntity();

        //lobbyP.InitData(player);
        //lobbyPlayerPrefabList.Add(lobbyP);
    }
    public override void OnLeftRoom() //Local Callback
    {
        //gamemanager.isPlayingGame = false;
    }
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer) //Other Players
    {
        //CustomActions._OnPlayerLeftRoom?.Invoke(otherPlayer.ActorNumber);

        //if (!gamemanager.isPlayingGame)
        //{
        //    int updatedTeamIndex = (int)otherPlayer.CustomProperties[NetworkPropertyKeys._PlayerUpdatedTeam];

        //    if (updatedTeamIndex != -1)
        //        gamemanager.uiController.EnableSelectedTeamColorButton(updatedTeamIndex);

        //    int index = lobbyPlayerPrefabList.FindIndex(x => x.thisPlayer == otherPlayer);

        //    if (index != -1)//If Exist
        //    {
        //        lobbyPlayerPrefabList[index].DestroyPrefab();
        //        lobbyPlayerPrefabList.RemoveAt(index);
        //    }
        //    PlayerCountCheck();
        //}
        //else
        //{
        //    RemovePlayersFromTurnList(otherPlayer);

        //    if (CheckIfLastPlayerLeft())
        //    {
        //        DeclareMyResult();
        //        return;
        //    }

        //    if (PhotonNetwork.IsMasterClient && (int)otherPlayer.CustomProperties[NetworkPropertyKeys._IsPlayerPlaying] == 1)
        //    {
        //        ++currentTurnIndex;

        //        if (currentTurnIndex >= _ListForTurn.Count)
        //            currentTurnIndex = 0;

        //        var player = _ListForTurn[currentTurnIndex];

        //        object[] data =
        //        {
        //                player.playerScript.ActorNumber,
        //                player.playerScript.NickName
        //            };

        //        NetworkRaiseEvent.RaiseEVT(NetworkEventCodes._PlayTurnCode, ReceiverGroup.All, data);
        //    }
        //}
    }
    private void PlayerCountCheck()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.CurrentRoom.IsOpen = PhotonNetwork.PlayerList.Length != PhotonNetwork.CurrentRoom.MaxPlayers;

        //gamemanager.uiController.UpdateLobbyPlayerCountOnUI(PhotonNetwork.PlayerList.Length, PhotonNetwork.CurrentRoom.MaxPlayers);
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        //HelperScript.LoadingPanel(false);
        //HelperScript.ShowNetworkError(returnCode);
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        currentLobbyRoomList.Clear();

        foreach (var room in roomList)
        {
            if (!room.RemovedFromList)
            {
                currentLobbyRoomList.Add(room);
            }
        }
    }
    internal List<RoomInfo> GetUpdatedRoomInfo()
    {
        return currentLobbyRoomList;
    }
    public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, Hashtable changedProps) //Global Callback
    {
        //if (changedProps.ContainsKey(NetworkPropertyKeys._PlayerUpdatedTeam))
        //{
        //    int updatedTeamIndex = (int)changedProps[NetworkPropertyKeys._PlayerUpdatedTeam];
        //    int currentTeamIndex = (int)changedProps[NetworkPropertyKeys._PlayerPreviousTeam];

        //    CustomActions._OnPlayerChangeTeam?.Invoke(targetPlayer.ActorNumber, currentTeamIndex, updatedTeamIndex);
        //}

        //if (changedProps.ContainsKey(NetworkPropertyKeys._PlayerReadyInLobby))
        //{
        //    int isReady = (int)changedProps[NetworkPropertyKeys._PlayerReadyInLobby];

        //    CustomActions._OnPlayerChangeReadyStatus?.Invoke(targetPlayer.ActorNumber, isReady == 1);

        //    if (PhotonNetwork.IsMasterClient)
        //        CheckIfAllPlayersReady();
        //}

        //if (changedProps.ContainsKey(NetworkPropertyKeys._IsPlayerPlaying))
        //{
        //    // Debug.Log($"Turn Assigned to {targetPlayer.NickName} As {(int)changedProps[NetworkPropertyKeys._PlayerIsPlaying]}");
        //    if ((int)changedProps[NetworkPropertyKeys._IsPlayerPlaying] == 1)
        //    {
        //        if (PhotonNetwork.LocalPlayer == targetPlayer)
        //        {
        //            gamemanager.PlayMyTurn();
        //        }
        //    }
        //    else
        //    {
        //        // Debug.Log($"Playing set to 0 for {targetPlayer.NickName}");
        //        if (PhotonNetwork.IsMasterClient)
        //        {
        //            if (CheckIfLastPlayerLeft())
        //            {
        //                DeclareMyResult();
        //                return;
        //            }

        //            ++currentTurnIndex;

        //            if (currentTurnIndex >= _ListForTurn.Count)
        //                currentTurnIndex = 0;

        //            var player = _ListForTurn[currentTurnIndex];

        //            object[] data =
        //            {
        //                    player.playerScript.ActorNumber,
        //                    player.playerScript.NickName
        //                };

        //            NetworkRaiseEvent.RaiseEVT(NetworkEventCodes._PlayTurnCode, ReceiverGroup.All, data);
        //        }
        //    }
        //}

        //if (changedProps.ContainsKey(NetworkPropertyKeys._PlayerWinPosition))
        //{
        //    if ((int)changedProps[NetworkPropertyKeys._PlayerWinPosition] == 0) return;

        //    RemovePlayersFromTurnList(targetPlayer);

        //    ResultPlayerEntity resultEntity = gamemanager.uiController.SpawnResultPlayerEntity();

        //    resultEntity.InitData(targetPlayer);

        //    if (targetPlayer.IsLocal)
        //    {
        //        gamemanager.uiController.ShowResultPanel();
        //        gamemanager.isPlayingGame = false;
        //    }
        //    else
        //    {
        //        if (CheckIfLastPlayerLeft())
        //            DeclareMyResult();
        //    }
        //}
        //if (changedProps.ContainsKey(NetworkPropertyKeys._PlayerSceneLoadingStatus))
        //{
        //    if (!PhotonNetwork.IsMasterClient) return;

        //    if ((int)changedProps[NetworkPropertyKeys._PlayerSceneLoadingStatus] == 0) return;

        //    ++sceneLoadedPlayersCount;

        //    if (sceneLoadedPlayersCount == PhotonNetwork.PlayerList.Length)
        //    {
        //        NetworkRaiseEvent.RaiseEVT(NetworkEventCodes._PlayerCanActivateScene, ReceiverGroup.All);
        //    }
        //}
    }
    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged) //Global Callbacks (Players in the rooms)
    {
        //if (propertiesThatChanged.ContainsKey(NetworkPropertyKeys._RoomLevelIndex))
        //{
        //    int levelIndex = (int)propertiesThatChanged[NetworkPropertyKeys._RoomLevelIndex];
        //    CustomActions._OnLevelChangedInLobby?.Invoke(levelIndex);
        //}
    }
    private void CheckIfAllPlayersReady()
    {
        //if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers && lobbyPlayerPrefabList.All(x => x.isReadyUp == true))
        //{
        //    PhotonNetwork.CurrentRoom.IsOpen = false;
        //    PhotonNetwork.CurrentRoom.IsVisible = false;
        //    NetworkRaiseEvent.RaiseEVT(NetworkEventCodes._SpawnPlayerEntityCode, ReceiverGroup.All);
        //}
    }
    private void DeclareMyResult()
    {
        //Hashtable prop = new();
        //prop.Add(NetworkPropertyKeys._PlayerWinPosition, gamemanager.uiController.playerResultList.Count + 1);
        //PhotonNetwork.LocalPlayer.SetCustomProperties(prop);
    }
    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == NetworkEventCodes._SpawnPlayerEntityCode)
        {
            //gamemanager.SpawnPlayerEntity();
            //CustomActions._OnShowLoadingPanel(true, "Loading Adventure...");
            //int levelIndex = (int)PhotonNetwork.CurrentRoom.CustomProperties[NetworkPropertyKeys._RoomLevelIndex];
            //gamemanager.LoadSceneWithDelay(levelIndex);
        }
        else if (photonEvent.Code == NetworkEventCodes._PlayTurnCode)
        {
            //var data = (object[])photonEvent.CustomData;
            //int actorNumber = (int)data[0];
            //string playerName = (string)data[1];
            //Debug.Log($"Turn changed, given to {playerName} Number {actorNumber}");

            //gamemanager.DisplayPlayerChanceOnScreen(actorNumber, playerName);

            //if (actorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
            //{
            //    Hashtable prop = new();
            //    prop.Add(NetworkPropertyKeys._IsPlayerPlaying, 1);
            //    PhotonNetwork.LocalPlayer.SetCustomProperties(prop);
            //}
        }
        else if (photonEvent.Code == NetworkEventCodes._PlayerCanActivateScene)
        {
            //gamemanager.ActivateLoadedScene();
        }
        //else if (photonEvent.Code == NetworkEventCodes._PlayerStartMovingPiece)
        //{
        //    Debug.Log("PlayerStartMovingPiece");

        //    var data = (object[])photonEvent.CustomData;
        //    int viewID = (int)data[0];
        //    int stepsCount = (int)data[1];

        //    CustomActions._OnPlayerStartMovingPiece?.Invoke(viewID, stepsCount);
        //}
    }
    internal void EndCurrentSession()
    {
        if (PhotonNetwork.InRoom)
            PhotonNetwork.LeaveRoom();

        PhotonNetwork.Disconnect();
    }
}