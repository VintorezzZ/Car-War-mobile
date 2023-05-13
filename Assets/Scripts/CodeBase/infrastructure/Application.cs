using UnityEngine;

namespace CodeBase.infrastructure
{
    public class Application : MonoBehaviour
    {
        public GameBootstrapper BootstrapperPrefab;
        private void Awake()
        {
            var bootstrapper = FindObjectOfType<GameBootstrapper>();
            if (bootstrapper == null)
                Instantiate(BootstrapperPrefab);
        }
    }
}