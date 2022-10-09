using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-10000)]
public class ServiceRegistrar : MonoBehaviour
{
    [SerializeField] MonoBehaviour _service;

    void Awake()
    {
    }
}
