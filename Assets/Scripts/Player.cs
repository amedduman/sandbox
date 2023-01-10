using System;
using Photon.Pun;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviourPun, IPunObservable
{
    [SerializeField] float _jumpStrength = 10;
    [SerializeField] Bullet _bulletPrefab;
    [SerializeField] LayerMask _groundlayer;
    [SerializeField] TMP_Text _playerNameText;
        
    
    private Color _currentColor;
    Rigidbody _rb;

    private void Awake()
    {
        if (photonView.IsMine)
        {
            _playerNameText.text = PhotonNetwork.NickName;
        }
        else
        {
            _playerNameText.text = GetComponent<PhotonView>().Owner.NickName;
        }
        _currentColor = GetComponentInChildren<Renderer>().material.color;
        _rb = GetComponent<Rigidbody>();
        
        if(transform.position.x > 0)
            _playerNameText.transform.localRotation = Quaternion.Euler(0, 180, 0);
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
            if(IsGrounded())
                _rb.AddForce(Vector3.up * _jumpStrength, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        if (Physics.CheckSphere(transform.position, .2f, _groundlayer))
            return true;
        return false;
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            photonView.RPC(nameof(SpawnBullet), RpcTarget.Others);
        }
    }

    [PunRPC]
    void SpawnBullet()
    {
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