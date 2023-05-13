using CodeBase.infrastructure.Service;
using CodeBase.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Ui
{
    public class ProgressSaverComponent : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;

        private void Awake()
        {
            _saveLoadService = DIContainer.Container.Single<ISaveLoadService>();
        }

        public void Save()
        {
            _saveLoadService.SaveProgress();
        }
    }
}