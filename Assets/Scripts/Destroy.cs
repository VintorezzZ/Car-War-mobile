using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
