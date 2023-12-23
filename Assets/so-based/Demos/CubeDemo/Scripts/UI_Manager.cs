using System;
using SystemUI;
using UnityEngine;

namespace CubeDemo
{
    public class UI_Manager : MonoBehaviour
    {
        [SerializeField] VisibilityHandler _visibilityHandler;
        [SerializeField] CubeDeadEvent _cubeDeadEvent;

        void OnEnable()
        {
            _cubeDeadEvent.AddListener(OnCubeDead);
        }

        void OnDisable()
        {
            _cubeDeadEvent.RemoveListener(OnCubeDead);
        }

        void OnCubeDead()
        {
            _visibilityHandler.Show(UI_ID.GameEnd, ShowMethod.Single);
        }
    }
}