using CodeBase.Logic;
using UnityEngine;

namespace infrastructure
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private const string INITIAL_POINT_TAG = "InitialPoint";
        private const string PLAYER_PATH = "Player/Player";
        private const string MAIN_MENU_HUD_PATH = "Ui/HUD/MainMenuHUD";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
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
            var initialPoint = GameObject.FindWithTag(INITIAL_POINT_TAG);
            var player = Instantiate(PLAYER_PATH, initialPoint.transform.position);
            var mainMenuWindow = Instantiate(MAIN_MENU_HUD_PATH).GetComponent<MainMenuWindow>();
            mainMenuWindow.Show(_sceneLoader);

            _stateMachine.Enter<GameLoopState>();
        }

        private GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            var instance = Object.Instantiate(prefab, at, Quaternion.identity);
            return instance;
        }

        private GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            var instance = Object.Instantiate(prefab);
            return instance;
        }
    }
}