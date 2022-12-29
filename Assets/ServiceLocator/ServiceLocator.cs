using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    [DefaultExecutionOrder(-100000)]
    public class ServiceLocator : MonoBehaviour
    {
        public static ServiceLocator Instance { get; private set;} = null;
        readonly Dictionary<Type, object> Services = new Dictionary<Type, object>();

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
        public void Register<TService>(TService service, bool IsSingleton) where TService : class, new()
        {
            if (IsSingleton && IsRegistered(service))
            {
                Debug.Break();
                throw new ServiceLocatorException($"{service.GetType()} has already registered as singleton. " +
                                                  $"Multiple registration of a singleton object is not allowed");
            }
            Services[service.GetType()] = service;
        }

        public void DeRegister <TService>(TService service) where TService : class, new()
        {
            Services.Remove(service.GetType());
        }

        public TService Get<TService>() where TService : class, new()
        {
            if (IsRegistered<TService>())
            {
                return (TService)Services[typeof(TService)];
            }
            
            Debug.Break();
            throw new ServiceLocatorException($"{typeof(TService)} hasn't been registered.");
        }

        bool IsRegistered<TService>()
        {
            return Services.ContainsKey(typeof(TService));
        }
        bool IsRegistered<TService>(TService service)
        {
            return Services.ContainsKey(service.GetType());
        }
    }

    public class ServiceLocatorException : Exception
    {
        public ServiceLocatorException(string message) : base(message) { }
    }

}
