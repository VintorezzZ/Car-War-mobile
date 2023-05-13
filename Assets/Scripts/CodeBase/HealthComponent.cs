using System;
using MoreMountains.Tools;
using UnityEngine;

namespace CodeBase
{
    public class HealthComponent : MonoBehaviour, IDamageable
    {
        [SerializeField]  private bool _godMode;
        [SerializeField] private int _startHealth;
        private float _health;
        private MMHealthBar _mmHealthBar;
        public float Health => _health;

        public Action<float> DamageTaken;

        private void Start()
        {
            _health = _startHealth;
            _mmHealthBar = GetComponent<MMHealthBar>();
        }

        public void TakeDamage(float damage)
        {
            if (_godMode)
            {
                DamageTaken?.Invoke(damage);
                return;
            }

            _health -= damage;
            _mmHealthBar.UpdateBar(_health, 0, 100, _health > 0);

            if (_health <= 0)
                _health = 0;

            DamageTaken?.Invoke(damage);
        }
    }
}