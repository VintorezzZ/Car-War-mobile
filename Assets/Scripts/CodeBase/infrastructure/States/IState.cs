    namespace CodeBase.infrastructure.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }

    public interface IPayLoadedState<in TPayLoad> : IExitableState
    {
        void Enter(TPayLoad payload);
    }

    public interface IExitableState
    {
        void Exit();
    }
}