using Services;

namespace infrastructure
{
    public class BootstrapState : IState
    {
        private const string INITIAL = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load(INITIAL, EnterLoadLevel);
        }

        public void Exit()
        {

        }

        private void EnterLoadLevel() => _stateMachine.Enter<LoadLevelState, string>("Start");

        private void RegisterServices()
        {
            Game.InputService = RegisterInputService();
        }

        private IInputService RegisterInputService()
        {
            return new InputService();
        }
    }
}