using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fabwelt
{
    public class LobbyPlayer : MonoBehaviour
    {
        [SerializeField] Image playerTeamLogo;
        [SerializeField] Image readyUpIMG;
        [SerializeField] Text playerName;
        [SerializeField] internal Photon.Realtime.Player thisPlayer;
        [SerializeField] internal bool isReadyUp;

        private void Start()
        {
            CustomActions._OnPlayerChangeReadyStatus += OnUpdateReadyStatus;
            CustomActions._OnDisposeObjectsOnLeavingNetwork += DestroyPrefab;
        }
        private void OnDestroy()
        {
            CustomActions._OnPlayerChangeReadyStatus -= OnUpdateReadyStatus;
            CustomActions._OnDisposeObjectsOnLeavingNetwork -= DestroyPrefab;
        }
        internal void InitData(Photon.Realtime.Player player)
        {
            thisPlayer = player;
            playerName.text = player.NickName;

            isReadyUp = (int)player.CustomProperties[NetworkPropertyKeys._PlayerReadyInLobby] == 1;

            readyUpIMG.gameObject.SetActive(isReadyUp);

            if (!thisPlayer.IsLocal)
            {
                int index = (int)player.CustomProperties[NetworkPropertyKeys._PlayerUpdatedTeam];

                if (index == -1) return;
            }
        }
        private void OnUpdateReadyStatus(int actorNumber, bool isReady)
        {
            if (actorNumber == thisPlayer.ActorNumber)
            {
                isReadyUp = isReady;
                readyUpIMG.gameObject.SetActive(isReadyUp);
            }
        }
        internal void DestroyPrefab()
        {
            Destroy(gameObject);
        }
    }
}