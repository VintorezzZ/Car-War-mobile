using System;
using CodeBase.infrastructure.AssetManagement;
using CodeBase.infrastructure.Factory;
using CodeBase.infrastructure.Service;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;

namespace CodeBase.infrastructure.States
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

        private void EnterLoadLevel() => _stateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            _services.Register<IInputService>(CreateInputService());
            _services.Register<IPersistentProgressService>(typeof(PersistentProgressService));
            _services.Register<ISaveLoadService>(typeof(SaveLoadService));
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