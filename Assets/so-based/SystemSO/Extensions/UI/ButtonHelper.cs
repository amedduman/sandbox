using System;
using System.Collections;
using System.Collections.Generic;
using SystemSO;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonHelper : MonoBehaviour
{
    [SerializeField] EventSO<Empty> _event;
    Button _button;

    void Awake()
    {
        _button = GetComponent<Button>();
    }

    void OnEnable()
    {
        _button.onClick.AddListener(InvokeEvent);
    }

    void OnDisable()
    {
        _button.onClick.RemoveListener(InvokeEvent);
    }

    void InvokeEvent()
    {
        Empty empty = new Empty();
        _event.Invoke(empty);
    }
}
