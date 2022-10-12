using System;
using UnityEngine;
using Services;

public class InputManager : MonoBehaviour
{
    public event Action OnJumpButtonPressed;
    public event Action OnFirstLevelLoadButtonPressed;
    public event Action OnSecondLevelLoadButtonPressed;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpButtonPressed?.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnFirstLevelLoadButtonPressed?.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnSecondLevelLoadButtonPressed?.Invoke();
        }
    }
}
