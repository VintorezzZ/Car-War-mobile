using CodeBase.Data;
using CodeBase.infrastructure.Factory;
using CodeBase.infrastructure.Service;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string PROGRESS_KEY = "Progress";

        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        [Inject]
        public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (var progressSaver in _gameFactory.ProgressSavers)
                progressSaver.SaveProgress(_progressService.Progress);

            PlayerPrefs.SetString(PROGRESS_KEY, _progressService.Progress.ToJson());
        }

        public PlayerProgress LoadProgress()
        {
            return PlayerPrefs.GetString(PROGRESS_KEY)?.ToDeserialized<PlayerProgress>();
        }
    }
}