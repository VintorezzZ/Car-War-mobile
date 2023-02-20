using System;
using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public abstract class BaseCar : MonoBehaviour
{
    private HealthComponent _healthComponent;
    private PoolItem _selfPoolItem;

    public HealthComponent HealthComponent => _healthComponent;

    protected void Start()
    {
        _healthComponent = GetComponent<HealthComponent>();
        Subscribe();
    }

    protected virtual void OnDamageTaken(float damage)
    {
        if (HealthComponent.Health > 0)
            return;

        var explosionFx = PoolManager.Get(PoolType.Explosion_fx);
        explosionFx.transform.position = transform.position;
        explosionFx.transform.rotation = transform.rotation;
        explosionFx.gameObject.SetActive(true);

        if (_selfPoolItem)
        {
            PoolManager.Return(_selfPoolItem);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        var damage =DamageCalculator.CalculateDamage(this, collision);

        if (collision.transform.CompareTag("Wall"))
        {
            _healthComponent.TakeDamage(damage);
        }
        else if (collision.transform.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(damage);
            _healthComponent.TakeDamage(damage);
        }
    }

    protected virtual void Subscribe()
    {
        _healthComponent.DamageTaken += OnDamageTaken;
    }

    protected virtual void UnSubscribe()
    {
        _healthComponent.DamageTaken -= OnDamageTaken;
    }

    private void OnDisable()
    {
        UnSubscribe();
    }
}
