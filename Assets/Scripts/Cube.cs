using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Services;

public class Cube : MonoBehaviour
{
    InputManager _inputManager;


    void Awake()
    {
        // ServiceLocator.Instance.Register(this);
    }

    void Start()
    {
        _inputManager = ServiceLocator.Instance.Get<InputManager>();

        _inputManager.OnJumpButtonPressed += CloseMe;
    }

    void OnDisable()
    {
        _inputManager.OnJumpButtonPressed -= CloseMe;
    }

    public void CloseMe()
    {
        gameObject.SetActive(false);
    }
}
