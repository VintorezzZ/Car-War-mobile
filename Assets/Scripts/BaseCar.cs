using System;
using UnityEngine;

public abstract class BaseCar : MonoBehaviour
{
    private PoolItem _selfPoolItem;
  
    private int _health; 
    [SerializeField] protected int maxHealth;
    protected bool godMode;
    
    private void Start()
    {
        _health = maxHealth;
        _selfPoolItem = GetComponent<PoolItem>();
    }

    public virtual void GetDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            _health = 0;
            var explosionFx = PoolManager.Get(PoolType.Explosion_fx);
            explosionFx.gameObject.SetActive(true);
            explosionFx.transform.position = transform.position;
            explosionFx.transform.rotation = transform.rotation;

            if (_selfPoolItem)
            {
                PoolManager.Return(_selfPoolItem);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
