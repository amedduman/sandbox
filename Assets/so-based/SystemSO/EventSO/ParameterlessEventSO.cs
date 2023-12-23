using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SystemSO
{
    public class ParameterlessEventSO : ScriptableObject
    {
        List<(Action, int)> _listeners = new();
        [field: SerializeField] bool DoListenerOrdering { get; set; } = false;
    
        public void AddListener(Action listener ,int order = 0)
        {
            _listeners.Add((listener, order));
        }

        public void RemoveListener(Action listenerToRemove)
        {
            int order = 0;
            foreach (var listener in _listeners)
            {
                if (listener.Item1 == listenerToRemove)
                {
                    order = listener.Item2;
                }
            }
            _listeners.Remove((listenerToRemove, order));
        }

        public void Invoke()
        {
            if (DoListenerOrdering)
            {
                InvokeListenersInOrder();
                return;
            }
        
            foreach (var listener in _listeners)
            {
                listener.Item1();
            }
        }

        void InvokeListenersInOrder()
        {
            var x = _listeners.OrderBy(i => i.Item2);
            foreach (var v in x)
            {
                v.Item1();
            }
        }
    }
}