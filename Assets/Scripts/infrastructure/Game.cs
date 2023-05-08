using CodeBase.Logic;
using infrastructure.Service;
using infrastructure.States;

namespace infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, new ServicesLocator());
        }
    }
}