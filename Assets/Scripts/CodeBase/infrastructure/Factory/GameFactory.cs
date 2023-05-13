using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.infrastructure.AssetManagement;
using CodeBase.infrastructure.Service;
using CodeBase.Ui;
using UnityEngine;

namespace CodeBase.infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;

        public List<IProgressReader> ProgressReaders { get; } = new List<IProgressReader>();
        public List<IProgressSaver> ProgressSavers { get; } = new List<IProgressSaver>();

        [Inject]
        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }

        public GameObject CreatePlayer(GameObject at) => InstantiateRegistered(AssetPath.PLAYER_PATH, at.transform.position);

        public MainMenuWindow CreateHud() => InstantiateRegistered(AssetPath.MAIN_MENU_HUD_PATH).GetComponent<MainMenuWindow>();

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            var gameObject = _assets.Instantiate(prefabPath, at);
            RegisterProgressObservers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            var gameObject = _assets.Instantiate(prefabPath);
            RegisterProgressObservers(gameObject);
            return gameObject;
        }

        private void RegisterProgressObservers(GameObject gameObject)
        {
            foreach (var progressReader in gameObject.GetComponentsInChildren<IProgressReader>())
                Register(progressReader);
        }

        private void Register(IProgressReader progressReader)
        {
            if (progressReader is IProgressSaver progressSaver)
                ProgressSavers.Add(progressSaver);

            ProgressReaders.Add(progressReader);
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressSavers.Clear();
        }
    }
}