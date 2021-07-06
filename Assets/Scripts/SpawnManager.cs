using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public List<Transform> spawnPoints;
    //public List<GameObject> enemiesList;
    //public List<GameObject> deadList;
    //public GameObject enemies;
    private float timer = 10;
    
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {        
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) 
        {
            timer = 10;
            SpawnEnemies();
        }

        GameManager.Instance.UpdateTimer(timer);
    }

    void SpawnEnemies()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            var aiCar = PoolManager.Get(PoolType.AiCar);
            SetSpawnSettings(aiCar, spawnPoint);
        }
    }

    private Transform SetSpawnSettings(PoolItem item, Transform point)
    {
        Transform itemTransform = item.gameObject.transform;
        
        itemTransform.SetParent(transform);
        itemTransform.position = point.position;
        itemTransform.rotation = point.rotation;
        
        item.gameObject.SetActive(true);

        return itemTransform;
    }
}
