using System;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

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

            Initialize();
        }

        void Initialize()
        {
            foreach (var serviceType in ReflectionService.GetAllAutoRegisteredServices())
            {
                if (IsRegistered(serviceType)) continue;
                
                if (serviceType.IsMonoBehaviour())
                {
                    FindOrCreateMonoService(serviceType);
                }
                else
                {
                    RegisterNewInstance(serviceType);
                }
            }
        }

        bool IsRegistered(Type t)
        {
            return Services.ContainsKey(t);
        }

        void RegisterNewInstance(Type serviceType)
        {
            Services[serviceType] = Activator.CreateInstance(serviceType);
        }
        
        object FindOrCreateMonoService(Type serviceType)
        {
            var inGameService = FindObjectOfType(serviceType);
            if (inGameService == null)
            {
                var newObject = new GameObject();
                newObject.AddComponent(serviceType);
                newObject.name = serviceType.Name;
                inGameService = newObject.GetComponent(serviceType);
            }
            Services[serviceType] = inGameService;
            return inGameService;
        }

        public void Register<TService>(TService service, bool safe = true) /*where TService : class, new()*/
        {
            var serviceType = typeof(TService);
            if (IsRegistered<TService>() && safe)
            {
                throw new ServiceLocatorException($"{serviceType.Name} has been already registered.");
            }

            Services[typeof(TService)] = service;
        }

        public TService Get<TService>() where TService : class, new()
        {
            var serviceType = typeof(TService);
            if (IsRegistered<TService>())
            {
                return (TService)Services[serviceType];
            }

            throw new ServiceLocatorException($"{serviceType.Name} hasn't been registered.");
        }

        bool IsRegistered<TService>()
        {
            return Services.ContainsKey(typeof(TService));
        }
    }

    public class ServiceLocatorException : Exception
    {
        public ServiceLocatorException(string message) : base(message) { }
    }

}
