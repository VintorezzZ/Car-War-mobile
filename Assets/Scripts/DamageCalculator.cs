using UnityEngine;

namespace DefaultNamespace
{
    public class DamageCalculator
    {
        private static Vector3 collisionVector = Vector3.zero;				// Collision vector.
        private static Vector3 collisionPos = Vector3.zero;					// Collision position.
        private static Quaternion collisionRot = Quaternion.identity;	// Collision rotation.
        private static float minimumCollisionForce = 5f;
        private static float damageMultiplier = 1f;


        public static float CalculateDamage(BaseCar car, Collision collision)
        {
            if (collision.contacts.Length < 1 || collision.relativeVelocity.magnitude < minimumCollisionForce)
                return 0;
            
            var colRelVel = collision.relativeVelocity;
            colRelVel *= 1f - Mathf.Abs(Vector3.Dot(car.transform.up, collision.contacts[0].normal));

            var cos = Mathf.Abs(Vector3.Dot(collision.contacts[0].normal, colRelVel.normalized));

            if (colRelVel.magnitude * cos < minimumCollisionForce)
                return 0;

            collisionVector = car.transform.InverseTransformDirection(colRelVel) / (damageMultiplier / 50f);

            collisionPos -= collisionVector * 5f;
            collisionRot = Quaternion.Euler(new Vector3(-collisionVector.z * 10f, -collisionVector.y * 10f, -collisionVector.x * 10f));

            var damage = colRelVel.magnitude * cos;

            //Debug.LogError(damage);

            return damage;
        }
    }
}