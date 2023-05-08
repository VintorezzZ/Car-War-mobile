using infrastructure.AssetManagement;
using infrastructure.Service;
using UnityEngine;

namespace infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;

        [Inject]
        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }
        
        public GameObject CreatePlayer(GameObject at) => _assets.Instantiate(AssetPath.PLAYER_PATH, at.transform.position);

        public MainMenuWindow CreateHud() => _assets.Instantiate(AssetPath.MAIN_MENU_HUD_PATH).GetComponent<MainMenuWindow>();
    }
}