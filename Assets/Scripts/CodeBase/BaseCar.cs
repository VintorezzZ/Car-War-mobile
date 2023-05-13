using CodeBase.Data;
using CodeBase.Pool;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase
{
    [RequireComponent(typeof(HealthComponent))]
    public abstract class BaseCar : MonoBehaviour, IProgressSaver
    {
        private HealthComponent _healthComponent;
        private PoolItem _selfPoolItem;
        private Rigidbody _rigidbody;

        public HealthComponent HealthComponent => _healthComponent;
        public Rigidbody Rigidbody => _rigidbody;

        protected virtual void Awake()
        {
            _healthComponent = GetComponent<HealthComponent>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
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

        public void SaveProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel = new PositionOnLevel(SceneManager.GetActiveScene().name, transform.position.AsVector3Data());
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (SceneManager.GetActiveScene().name == progress.WorldData.PositionOnLevel.Level)
            {
                var savedPosition = progress.WorldData.PositionOnLevel.Position;
                if (savedPosition != null)
                    Warp(to: savedPosition);
            }
        }

        private void Warp(Vector3Data to)
        {
            // characterController.enabled = false
            _rigidbody.isKinematic = true;
            transform.position = to.AsUnityVector().AddY(0.5f);
            _rigidbody.isKinematic = false;
            // characterController.enabled = true
        }
    }
}