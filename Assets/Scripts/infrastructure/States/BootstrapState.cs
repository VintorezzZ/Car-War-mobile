using System;
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
        private readonly IDIContainer _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, IDIContainer services)
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
            _services.Register<IInputService>(CreateInputService());
            _services.Register<IAssets>(typeof(AssetProvider));
            _services.Register<IGameFactory>(typeof(GameFactory));

            _services.Build();
        }

        private Type CreateInputService()
        {
            return typeof(InputService);
        }
    }
}