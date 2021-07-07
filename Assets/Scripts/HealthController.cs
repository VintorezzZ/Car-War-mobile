using UnityEngine;

namespace DefaultNamespace
{
    public class HealthController
    {
        private float _index = 0f;
        
        private Vector3 collisionVector = Vector3.zero;				// Collision vector.
        private Vector3 collisionPos = Vector3.zero;					// Collision position.
        private Quaternion collisionRot = Quaternion.identity;	// Collision rotation.
        private float minimumCollisionForce = 5f;
        private float damageMultiplier = 1f;


        public void CalculateDamage(BaseCar player, Collision collision)
        {
            if (collision.contacts.Length < 1 || collision.relativeVelocity.magnitude < minimumCollisionForce)
                return;
            
            Vector3 colRelVel = collision.relativeVelocity;
            colRelVel *= 1f - Mathf.Abs(Vector3.Dot(player.transform.up, collision.contacts[0].normal));

            float cos = Mathf.Abs(Vector3.Dot(collision.contacts[0].normal, colRelVel.normalized));

            if (colRelVel.magnitude * cos >= minimumCollisionForce){

                collisionVector = player.transform.InverseTransformDirection(colRelVel) / (damageMultiplier / 50f);

                collisionPos -= collisionVector * 5f;
                collisionRot = Quaternion.Euler(new Vector3(-collisionVector.z * 10f, -collisionVector.y * 10f, -collisionVector.x * 10f));
                _index = Mathf.Clamp((colRelVel.magnitude * cos) * 50f, 0f, 10f);
                Debug.LogError(colRelVel.magnitude * cos + " " + _index);
            }
        }
    }
}