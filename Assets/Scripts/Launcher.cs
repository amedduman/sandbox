using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _connectPnl;
    [SerializeField] private GameObject _connectedPnl;
    [SerializeField] private GameObject _disconnectedPnl;
    [SerializeField] private TMP_InputField _roomNameInputField;
    
    
    public void OnClick_ConnectToServer()
    {
        _connectPnl.SetActive(false);
        PhotonNetwork.ConnectUsingSettings();
    }
    
    public void OnClick_EnterRoom()
    {
        string room = _roomNameInputField.text;
        if(string.IsNullOrEmpty(room)) return;
        _connectedPnl.SetActive(false);
        PhotonNetwork.JoinOrCreateRoom(room, new RoomOptions(), TypedLobby.Default);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        _connectPnl.SetActive(false);
        _connectedPnl.SetActive(false);
        _disconnectedPnl.SetActive(true);
    }

    public override void OnJoinedLobby()
    {
        _connectedPnl.SetActive(true);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
