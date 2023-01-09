using Photon.Pun;
using UnityEngine;

public class PlayerSpawnController : MonoBehaviour
{
    [SerializeField] GameObject _playerPrefab;
    
    void Start()
    {
        PhotonNetwork.Instantiate(_playerPrefab.name, Vector3.zero, Quaternion.identity);
    }
}
