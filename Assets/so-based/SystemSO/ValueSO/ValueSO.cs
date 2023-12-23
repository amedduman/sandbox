using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueSO<T> : ScriptableObject
{
    public T Value;
    public T DefaultValue;

    void OnEnable()
    {
        Value = DefaultValue;
    }
}
