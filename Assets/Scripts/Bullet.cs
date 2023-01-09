using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rb;
    public Vector3 Direction;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 pos = _rb.position;
        pos += .01f * Time.fixedTime * Direction;
        _rb.MovePosition(pos);
    }
}