using System;
using UnityEngine;
using UnityEngine.Events;

namespace SystemUI
{
    public class UI_Element : MonoBehaviour
    {
        [field:SerializeField] public UI_ID ID { get; private set; }
        [SerializeField] StartingState _startingState;
        [SerializeField] UnityEvent _onShow;
        [SerializeField] UnityEvent _onHide;

        void Awake()
        {
            switch (_startingState)
            {
                case StartingState.DoNothing:
                    break;
                case StartingState.Hide:
                    Hide();
                    break;
                case StartingState.Show:
                    Show();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Hide()
        {
            _onHide?.Invoke();
        }

        public void Show()
        {
            _onShow?.Invoke();
        }
    }
}

enum StartingState
{
    DoNothing = 0,
    Hide = 10,
    Show = 20,
}