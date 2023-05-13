namespace CodeBase.Data
{
    public interface IProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }

    public interface IProgressSaver : IProgressReader
    {
        void SaveProgress(PlayerProgress progress);
    }
}