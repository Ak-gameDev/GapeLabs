using Fabwelt;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomActions
{
    #region Game Play


    #endregion

    public static Action _OnDisposeObjectsOnLeavingNetwork;

    #region UI

    public static Action<bool, string> _OnShowLoadingPanel;
    public static Action<string> _OnShowNotification;

    #endregion

    #region AUDIO

    //public static Action<AudioClip, AudioPlaybackType> _OnPlayAudioWithClip;
    //public static Action<AudioNature, AudioPlaybackType> _OnPlayAudioWithNature;

    #endregion

    #region MULTIPLAYER

    public static Action<int, int> _OnPlayerBeforeChangeTeam;
    public static Action<int, int, int> _OnPlayerChangeTeam;
    public static Action<int, bool> _OnPlayerChangeReadyStatus;
    //public static Action<PieceEntity> _OnPieceSpawned;
    //public static Action<PieceEntity> _OnPieceDespawned;
    public static Action<int> _OnLevelChangedInLobby;
    public static Action<int> _OnPlayerLeftRoom;
    //public static Action<int, int> _OnPlayerStartMovingPiece;

    #endregion
}