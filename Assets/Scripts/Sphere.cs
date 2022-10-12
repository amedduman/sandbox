using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Services;

public class Sphere : MonoBehaviour
{
    InputManager _inputManager;
    MyCube _cube;


     void Start()
    {
        _inputManager = ServiceLocator.Instance.Get<InputManager>();
        _cube = ServiceLocator.Instance.Get<MyCube>();

        _inputManager.OnJumpButtonPressed += CloseMe;
    }

    void OnDisable()
    {
        _inputManager.OnJumpButtonPressed -= CloseMe;
    }

    public void CloseMe()
    {
        gameObject.SetActive(false);
        // _cube.gameObject.SetActive(true);
        _cube.transform.GetChild(0).gameObject.SetActive(true);
    }
}
