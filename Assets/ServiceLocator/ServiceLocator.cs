using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    [DefaultExecutionOrder(-100000)]
    public class ServiceLocator : MonoBehaviour
    {
        public static ServiceLocator Instance { get; private set;} = null;
        readonly Dictionary<Type, object> SingletonServices = new Dictionary<Type, object>();
        private readonly Dictionary<SerLocID, object> Services = new Dictionary<SerLocID, object>();

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
        public void Register<TService>(TService service) where TService : class, new()
        {
            if (SingletonServices.TryGetValue(service.GetType(), out object srv))
            {
                if (IsNullOrDestroyed(srv))
                {
                    SingletonServices[service.GetType()] = service;
                }
                else
                {
                    Debug.Break();
                    throw new ServiceLocatorException($"{service.GetType()} has already registered as singleton. " +
                                                      $"Multiple registration of a singleton object is not allowed");
                }
            }
            else
            {
                SingletonServices[service.GetType()] = service;
            }
        }
        
        public void Register<TService>(TService service, SerLocID id) where TService : Component
        {
            if (Services.ContainsKey(id))
            {
                if (IsNullOrDestroyed(Services[id]))
                {
                    Services[id] = service;
                }
                else
                {
                    Debug.Break();
                    throw new ServiceLocatorException(
                        $"{service.GetType()} has already registered with the tag \"{id}\"");
                }
            }
            else
            {
                Services[id] = service;
            }
        }
        
        bool IsNullOrDestroyed(System.Object obj) 
        {
            if (ReferenceEquals(obj, null)) 
                return true;
            
            if (obj is UnityEngine.Object) 
                return (obj as UnityEngine.Object) == null;
            
            return false;
        }

        public TService Get<TService>() where TService : class, new()
        {
            if (SingletonServices.TryGetValue(typeof(TService), out object srv))
            {
                return (TService)srv;
            }
            Debug.Break();
            throw new ServiceLocatorException($"{typeof(TService)} hasn't been registered.");
        }
        
        public TService Get<TService>(SerLocID id) where TService : Component
        {
            if (Services.TryGetValue(id, out object srv))
            {
                return (TService)srv;
            }
            Debug.Break();
            throw new ServiceLocatorException($"An Instance of {typeof(TService)} with the tag \"{id}\" hasn't been registered.");
        }
    }

    public class ServiceLocatorException : Exception
    {
        public ServiceLocatorException(string message) : base(message) { }
    }
}
