using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-90000)]
public class MonoService<T> : MonoBehaviour where T : MonoBehaviour
{
    T instance;
    protected virtual void Awake()
    {
        instance = this as T;
        Services.ServiceLocator.Instance.Register(instance);
    }
}
