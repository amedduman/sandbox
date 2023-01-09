using System;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviourPun, IPunObservable
{
    [SerializeField] float _jumpStrength = 10;
    private Color _currentColor;
    Rigidbody _rb;

    private void Awake()
    {
        _currentColor = GetComponentInChildren<Renderer>().material.color;
        _rb = GetComponent<Rigidbody>();

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            _rb.position = new Vector3(-10, 0, 0);
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            _rb.position = new Vector3(10, 0, 0);
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            Jump();

            ChangeColor();
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