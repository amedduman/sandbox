using System;
using Services;
using UnityEngine;

public class ExtendedMB : MonoBehaviour
{
    [HideInInspector] public AutoRegister MyAutoRegisterer;
    
    protected void OnDestroy()
    {
        // MyAutoRegisterer.DestroyProcess();
    }
}