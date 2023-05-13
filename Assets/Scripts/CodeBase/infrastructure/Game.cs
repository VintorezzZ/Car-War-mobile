using CodeBase.infrastructure.Service;
using CodeBase.infrastructure.States;

namespace CodeBase.infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, new DIContainer());
        }
    }
}