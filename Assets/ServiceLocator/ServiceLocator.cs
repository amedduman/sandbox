using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    public struct KeyData
    {
        Type MyType;
        int Id;

        public KeyData(Type type, int id)
        {
            MyType = type;
            Id = id;
        }
    }

    [DefaultExecutionOrder(-100000)]
    public class ServiceLocator : MonoBehaviour
    {
        
        public static ServiceLocator Instance { get; private set;} = null;
        readonly Dictionary<KeyData, object> Services = new Dictionary<KeyData, object>();

        void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError($"Found duplicate {nameof(ServiceLocator)} on {gameObject.name}");
                Destroy(gameObject);
                Debug.Break();
                return;
            }

            Instance = this;
        }
        public void Register<TService>(TService service, int id, bool safe = true) /*where TService : class, new()*/
        {
            KeyData keyData = new KeyData(service.GetType(), id);
            if (IsRegistered(keyData) && safe)
            {
                throw new ServiceLocatorException($"{service.GetType().Name} has been already registered.");
            }
            Services[keyData] = service;
        }

        public Component Get(Component service, int id) /*where TService : class, new()*/
        {
            var serviceType = service.GetType();
            KeyData keyData = new KeyData(service.GetType(), id);
            if (IsRegistered(keyData))
            {
                return (Component)Services[keyData];
            }

            throw new ServiceLocatorException($"{serviceType.Name} hasn't been registered.");
        }

        // bool IsRegistered(object o)
        // {
        //     return Services.ContainsKey(o.GetType());
        // }

        bool IsRegistered(KeyData keyData)
        {
            return Services.ContainsKey(keyData);
        }
    }

    public class ServiceLocatorException : Exception
    {
        public ServiceLocatorException(string message) : base(message) { }
    }

}
