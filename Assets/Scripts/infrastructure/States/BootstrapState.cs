using infrastructure.AssetManagement;
using infrastructure.Factory;
using infrastructure.Service;
using Services;

namespace infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string INITIAL = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ServicesLocator _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, ServicesLocator services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(INITIAL, EnterLoadLevel);
        }

        public void Exit()
        {

        }

        private void EnterLoadLevel() => _stateMachine.Enter<LoadLevelState, string>("Start");

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(CreateInputService());
            _services.RegisterSingle<IAssets>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssets>()));
        }

        private IInputService CreateInputService()
        {
            return new InputService();
        }
    }
}