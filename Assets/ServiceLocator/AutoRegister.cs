using System;
using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEngine;


[DefaultExecutionOrder(-10000)]
public class AutoRegister : MonoBehaviour
{
    [SerializeField] ExtendedMB _cmp;
    [SerializeField] private bool _isSingleton;
    
    void Awake()
    {
        _cmp.MyAutoRegisterer = this;
        ServiceLocator.Instance.Register(_cmp, _isSingleton); 
    }

    public void DestroyProcess()
    {
        ServiceLocator.Instance.DeRegister(_cmp);
    }
}
