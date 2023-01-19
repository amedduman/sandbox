using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    CanvasGroup _startGamePanel;
    TMP_InputField _playerNameField;
    Button _playButton;

    public override void OnEnable()
    {
        base.OnEnable();
        
        _playButton.onClick.AddListener(PlayGame);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        
        _playButton.onClick.RemoveAllListeners();
    }

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
        _startGamePanel = ServiceLocator.Get<CanvasGroup>(SerLocID.startPanel);
        _playerNameField = ServiceLocator.Get<TMP_InputField>(SerLocID.playerNameField);
        _playButton = ServiceLocator.Get<Button>(SerLocID.playButton);

        _startGamePanel.alpha = 0;
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        _startGamePanel.alpha = 1;
    }

    public void PlayGame()
    {
        if (string.IsNullOrEmpty(_playerNameField.text)) return;
        PhotonNetwork.NickName = _playerNameField.text;
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
