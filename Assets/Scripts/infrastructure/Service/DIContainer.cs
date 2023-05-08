using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace infrastructure.Service
{
    public class DIContainer : IDIContainer
    {
        private static IDIContainer _instance;
        public static IDIContainer Container => _instance ??= new DIContainer();

        private static readonly IDictionary<Type, Type> _mappings = new Dictionary<Type, Type>();
        private static readonly IDictionary<Type, IService> _transiets = new Dictionary<Type, IService>();

        public void Register<TService>(Type implementation) where TService : IService
        {
            _mappings[typeof(TService)] = implementation;
        }

        public TService Resolve<TService>() where TService : IService
        {
            return default;
        }

        private object Resolve(Type type)
        {
            return _transiets.ContainsKey(type) ? _transiets[type] : CreateInstance(_mappings[type]);
        }

        public TService Single<TService>() where TService : IService => (TService) _transiets[typeof(TService)];

        public void Build()
        {
            _transiets.Clear();

            foreach (var (type, implementationType) in _mappings)
                _transiets[type] = CreateInstance(implementationType);
        }

        private IService CreateInstance(Type type)
        {
            var injectConstructor = type.GetConstructors().SingleOrDefault(c => c.GetCustomAttributes<InjectAttribute>().Any());

            return injectConstructor == null
                ? (IService) Activator.CreateInstance(type)
                : (IService) injectConstructor.Invoke(injectConstructor.GetParameters().Select(p => Resolve(p.ParameterType)).ToArray());
        }
    }
}