using Photon.Pun;
using UnityEngine;

public class PlayerSpawnController : MonoBehaviour
{
    [SerializeField] GameObject _playerPrefab;
    
    void Start()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.Instantiate(_playerPrefab.name, new Vector3(-9, 0, 0), Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate(_playerPrefab.name, new Vector3(9, 0, 0), Quaternion.Euler(0, 180, 0));
        }
    }
}
