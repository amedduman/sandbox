using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _connectPnl;
    [SerializeField] private GameObject _connectedPnl;
    [SerializeField] private GameObject _disconnectedPnl;
    
    
    
    public void OnClick_ConnectToServer()
    {
        _connectPnl.SetActive(false);
        PhotonNetwork.ConnectUsingSettings();
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
}
