using System;
using System.Collections.Generic;
using System.Linq;

namespace Lockstep.Game
{

    public class ServiceContainer
    {
        private readonly Dictionary<string, IService> _instances = new Dictionary<string, IService>();

        public void Register(IService instance, bool overwriteExisting = true)
        {
            string[] interfaceNames = GetInterfaceNames(instance);
            foreach (string key in interfaceNames)
            {
                if (!_instances.ContainsKey(key))
                {
                    _instances.Add(key, instance);
                }
                else if (overwriteExisting)
                {
                    _instances[key] = instance;
                }
            }
        }

        public T Get<T>() where T : IService
        {
            string fullName = typeof(T).FullName;
            if (fullName == null)
            {
                return default(T);
            }
            if (!_instances.ContainsKey(fullName))
            {
                return default(T);
            }
            return (T)_instances[fullName];
        }

        private string[] GetInterfaceNames(object instance)
        {
            return (from type in instance.GetType().FindInterfaces((Type type, object criteria) => type.GetInterfaces().Any((Type t) => t.FullName == typeof(IService).FullName), instance)
                    select type.FullName).ToArray();
        }
    }

}
