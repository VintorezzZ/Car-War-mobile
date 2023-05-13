using System.Collections;
using CodeBase.Pool;
using UnityEngine;

namespace CodeBase
{
    public class Destroy : MonoBehaviour
    {
        private PoolItem _selfPoolItem;
        void Start()
        {
            _selfPoolItem = GetComponent<PoolItem>();

            StartCoroutine(DeathCoroutine(3f));
        }

        IEnumerator DeathCoroutine(float timeToDeath)
        {
            yield return new WaitForSeconds(timeToDeath);
            PoolManager.Return(_selfPoolItem);
        }
    }
}
