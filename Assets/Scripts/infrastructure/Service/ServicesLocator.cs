namespace infrastructure.Service
{
    public class ServicesLocator
    {
        private static ServicesLocator _instance;
        public static ServicesLocator Container => _instance ??= new ServicesLocator();

        public void RegisterSingle<TService>(TService implementation) where TService : IService =>
            Implementation<TService>.ServiceInstance = implementation;

        public TService Single<TService>() where TService : IService => Implementation<TService>.ServiceInstance;

        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}