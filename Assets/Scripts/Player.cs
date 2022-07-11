using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    private void Awake()
    {
        DependencyProvider.Instance.Register(this);
    }
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
