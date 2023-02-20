using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private GameObject hitEffect;
        [SerializeField] private float hitForce;
        [SerializeField] private float hitRadius;
        [SerializeField] private int damage = 25;

        public Rigidbody Rigidbody => _rigidbody;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);

                if (!collision.transform.TryGetComponent(out Rigidbody rb))
                    return;

                rb.AddExplosionForce(hitForce, transform.position, hitRadius);
            }

            var effect = Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(effect, 1f);

            Destroy(gameObject);
        }
    }
}