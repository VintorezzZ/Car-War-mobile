using CodeBase.Logic;
using infrastructure.Factory;
using infrastructure.Service;
using UnityEngine;

namespace infrastructure.States
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private const string INITIAL_POINT_TAG = "InitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private IGameFactory _gameFactory;

        [Inject]
        private void Inject(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            var player = _gameFactory.CreatePlayer(GameObject.FindWithTag(INITIAL_POINT_TAG));
            var mainMenuWindow = _gameFactory.CreateHud();
            mainMenuWindow.Show(_sceneLoader);

            _stateMachine.Enter<GameLoopState>();
        }
    }
}