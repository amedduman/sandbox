using Photon.Pun;
using UnityEngine;

public class Player : MonoBehaviourPun
{
    [SerializeField] float _jumpStrength = 10;
    Rigidbody _rb;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            Jump();
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * _jumpStrength, ForceMode.Impulse);
        }
    }
}