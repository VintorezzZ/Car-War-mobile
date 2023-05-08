using UnityEngine;

namespace infrastructure.AssetManagement
{
    public class AssetProvider : IAssets
    {
        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            var instance = Object.Instantiate(prefab);
            return instance;
        }

        public GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            var instance = Object.Instantiate(prefab, at, Quaternion.identity);
            return instance;
        }
    }
}