using System;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviourPun, IPunObservable
{
    [SerializeField] float _jumpStrength = 10;
    [SerializeField] Bullet _bulletPrefab;
    
    private Color _currentColor;
    Rigidbody _rb;

    private void Awake()
    {
        _currentColor = GetComponentInChildren<Renderer>().material.color;
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            Jump();

            ChangeColor();

            Shoot();
        }
    }

    private void ChangeColor()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _currentColor = Random.ColorHSV();
            UpdateColor();
        }
    }

    void UpdateColor()
    {
        GetComponentInChildren<Renderer>().material.color = _currentColor;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * _jumpStrength, ForceMode.Impulse);
        }
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("clicked");
            photonView.RPC(nameof(SpawnBullet), RpcTarget.Others);
        }
    }

    [PunRPC]
    void SpawnBullet()
    {
        Debug.Log("called");
        var bullet = PhotonNetwork.Instantiate(_bulletPrefab.name, transform.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.Direction = transform.right;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_currentColor.r);
            stream.SendNext(_currentColor.b);
            stream.SendNext(_currentColor.g);
            
        }
        else if (stream.IsReading)
        {
            Color col = Color.magenta;
            col.r = (float)stream.ReceiveNext();
            col.b = (float)stream.ReceiveNext();
            col.g = (float)stream.ReceiveNext();

            _currentColor = col;
            UpdateColor();
        }
    }
}