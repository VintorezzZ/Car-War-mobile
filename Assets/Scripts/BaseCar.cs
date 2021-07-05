using System;
using UnityEngine;

public abstract class BaseCar : MonoBehaviour
{
    private int _health; 
    [SerializeField] protected int maxHealth;
    public ParticleSystem expl;
    private void Start()
    {
        _health = maxHealth;
    }

    public virtual void GetDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            _health = 0;            
            Instantiate(expl, transform.position, transform.rotation);
            Destroy(this.gameObject, 0f);
        }
    }
}
