using System;

namespace infrastructure.Service
{
    public interface IDIContainer
    {
        protected static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }

        void Register<TService>(Type implementation) where TService : IService;
        TService Resolve<TService>() where TService : IService;
        TService Single<TService>() where TService : IService;
        void Build();
    }


    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method)]
    public class InjectAttribute : Attribute { }
}