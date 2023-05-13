using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.infrastructure.Service;
using CodeBase.Ui;
using UnityEngine;

namespace CodeBase.infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<IProgressReader> ProgressReaders { get; }
        List<IProgressSaver> ProgressSavers { get; }
        GameObject CreatePlayer(GameObject at);
        MainMenuWindow CreateHud();
        void Cleanup();
    }
}