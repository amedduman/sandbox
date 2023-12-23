using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SystemSO
{
    public class EventSO<T> : ScriptableObject
    {
        List<( 
            Func<T, UniTask>,
            int
            )> _listeners = new();
        
        [field: SerializeField] bool DoListenerOrdering { get; set; } = false;
        [field: SerializeField] bool DoWaitForListeners { get; set; } = false;

        public void AddListener(Func<T, UniTask> listener, int order = 0)
        {
            _listeners.Add((listener, order));
        }

        public void RemoveListener(Func<T, UniTask> listenerToRemove)
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

        public void Invoke(T data)
        {
            if (DoListenerOrdering)
            {
                if (DoWaitForListeners)
                {
                    InvokeAwaitableListenersInOrder(data);
                    return;
                }
                
                InvokeListenersInOrder(data);
                return;
            }
            
            foreach (var listener in _listeners)
            {
                listener.Item1(data);
            }
        }

        void InvokeListenersInOrder(T data)
        {
            var x = _listeners.OrderBy(i => i.Item2);
            foreach (var v in x)
            {
                v.Item1(data);
            }
        }

        async void InvokeAwaitableListenersInOrder(T data)
        {
            var x = _listeners.OrderBy(i => i.Item2);

            foreach (var listener in x)
            {
                await listener.Item1(data);
            }
        }
    }

}