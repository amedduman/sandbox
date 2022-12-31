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
        private readonly Dictionary<string, object> Services = new Dictionary<string, object>();

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
        
        public void Register<TService>(TService service, string myTag) where TService : Component
        {
            if (Services.ContainsKey(myTag))
            {
                if (IsNullOrDestroyed(Services[myTag]))
                {
                    Services[myTag] = service;
                }
                else
                {
                    Debug.Break();
                    throw new ServiceLocatorException(
                        $"{service.GetType()} has already registered with the tag \"{myTag}\"");
                }
            }
            else
            {
                Services[myTag] = service;
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
        
        public TService Get<TService>(string myTag) where TService : Component
        {
            if (Services.TryGetValue(myTag, out object srv))
            {
                return (TService)srv;
            }
            Debug.Break();
            throw new ServiceLocatorException($"An Instance of {typeof(TService)} with the tag \"{myTag}\" hasn't been registered.");
        }
    }

    public class ServiceLocatorException : Exception
    {
        public ServiceLocatorException(string message) : base(message) { }
    }
}
