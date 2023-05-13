using UnityEngine;

namespace CodeBase
{
    public class DamageCalculator
    {
        private static Vector3 collisionVector = Vector3.zero;				// Collision vector.
        private static Vector3 collisionPos = Vector3.zero;					// Collision position.
        private static Quaternion collisionRot = Quaternion.identity;	// Collision rotation.
        private static float minimumCollisionForce = 5f;
        private static float damageMultiplier = 1f;


        // public static float CalculateDamage(BaseCar car, Collision collision)
        // {
        //     float damage = 0;
        //
        //     // Get mass of each car
        //     float m1 = car.Rigidbody.mass;
        //     var rb2 = collision.gameObject.GetComponent<Rigidbody>();
        //     float m2 = rb2 != null ? rb2.mass : 100f;
        //
        //     // Get velocities of each car
        //     Vector3 v1 = car.Rigidbody.velocity;
        //     Vector3 v2 = rb2 != null ? rb2.velocity : Vector3.zero;
        //
        //     // Calculate relative velocity between two cars
        //     Vector3 relativeVelocity = v2 - v1;
        //
        //     // Get the distance between two cars
        //     float distance = Vector3.Distance(car.transform.position, collision.transform.position);
        //
        //     // Calculate time to impact
        //     float timeToImpact = distance / relativeVelocity.magnitude;
        //
        //     // Calculate the force applied to each car during impact
        //     float force1 = m1 * relativeVelocity.magnitude / timeToImpact;
        //     //float force2 = m2 * relativeVelocity.magnitude / timeToImpact;
        //
        //     // Calculate damage using a formula that takes into account mass, speed, and impact angle
        //     Vector3 direction = collision.transform.position - car.transform.position;
        //     float angle = Vector3.Angle(direction, car.transform.forward);
        //     float speed = relativeVelocity.magnitude;
        //     var mathPow = Mathf.Pow(speed, 2);
        //     var cos = Mathf.Cos(angle);
        //     float damage1 = m1 * speed * cos / (2 * m2);
        //     damage1 = Mathf.Abs(damage1);
        //     //float damage2 = m2 * Mathf.Pow(speed, 2) * Mathf.Cos(angle) / (2 * m1);
        //
        //     Debug.LogError($"{damage1}: {car.name}");
        //
        //     return damage1;
        // }
        public static float CalculateDamage(BaseCar car, Collision collision)
        {
            if (collision.contacts.Length < 1 || collision.relativeVelocity.magnitude < minimumCollisionForce)
                return 0;

            var colRelVel = collision.relativeVelocity;
            var collisionDot = Mathf.Abs(Vector3.Dot(car.transform.up, collision.contacts[0].normal));
            //colRelVel *= 1f - collisionDot;

            Vector3 direction = collision.transform.position - car.transform.position;
            float angle = Vector3.Angle(direction, car.transform.forward);
            var sin = Mathf.Sin(angle);

            var cos = Mathf.Abs(Vector3.Dot(collision.contacts[0].normal, colRelVel.normalized));

            var damage = colRelVel.magnitude * sin/* * collisionDot*/;

            if (damage < minimumCollisionForce)
                return 0;

            Debug.LogError($"{damage}: {car.name}");

            return damage;
        }
    }
}