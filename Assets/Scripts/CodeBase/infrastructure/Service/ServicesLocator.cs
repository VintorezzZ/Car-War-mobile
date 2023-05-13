using System;

namespace CodeBase.infrastructure.Service
{
    public class ServicesLocator : IDIContainer
    {
        private static IDIContainer _instance;
        public static IDIContainer Container => _instance ??= new ServicesLocator();

        public void Register<TService>(TService implementation) where TService : IService =>
            IDIContainer.Implementation<TService>.ServiceInstance = implementation;

        public void Register<TService>(Type implementation) where TService : IService
        {

        }

        public TService Resolve<TService>() where TService : IService => Single<TService>();
        public TService Single<TService>() where TService : IService => IDIContainer.Implementation<TService>.ServiceInstance;
        public void Build() { }
    }
}