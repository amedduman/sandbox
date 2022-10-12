using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Services;


public class MyCube : MonoBehaviour
{
    InputManager _inputManager;


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
        // gameObject.SetActive(false);
    }
}
