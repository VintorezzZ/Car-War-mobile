namespace CodeBase.Pool
{
    public interface IPoolObservable
    {
        void OnReturnToPool();
        void OnTakeFromPool();
    }
}
