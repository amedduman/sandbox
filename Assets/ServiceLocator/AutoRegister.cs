using System;
using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEngine;

[DefaultExecutionOrder(-10000)]
public class AutoRegister : MonoBehaviour
{    
    [SerializeField] bool _isSingleton;

    // [ShowIf(nameof(_isSingleton), true)]
    // [SerializeField] ExtendedMB _singletonCmp;
    
    // [ShowIf(nameof(_isSingleton), false)]
    [SerializeField] Component _cmp;
    [ShowIf(nameof(_isSingleton), false)]
    [SerializeField] string _tag;
    
    void Awake()
    {
        if (_isSingleton)
        {
            // _singletonCmp.MyAutoRegisterer = this;
            ServiceLocator.Instance.Register(_cmp); 
        }
        else
        {
            ServiceLocator.Instance.Register(_cmp, _tag); 
        }
    }

    // public void DestroyProcess()
    // {
    //     ServiceLocator.Instance.DeRegister(_singletonCmp);
    // }
}
