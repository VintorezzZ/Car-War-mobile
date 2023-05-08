using infrastructure.Service;
using UnityEngine;

namespace infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer(GameObject at);
        MainMenuWindow CreateHud();
    }
}