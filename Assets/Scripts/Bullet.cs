using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public GameObject explosionSound;
    public GameObject hitEffect;
    private bool hit = false;
    private Rigidbody rb;
    public float explosionForce;
    public float explosionRadius;
    public int damage = 25;

    private void OnCollisionEnter(Collision collision)
    {
        if (!hit)
        {            
            rb = collision.other.GetComponentInChildren<Rigidbody>();
            if (rb != null) 
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
           
            //Instantiate(explosionSound, transform.position, transform.rotation);
            GameObject go = Instantiate(hitEffect, transform.position, transform.rotation);
            //Destroy(explosionSound, 4f);
            Destroy(go, 1f);
            hit = true;

            EntityCar hp = collision.gameObject.GetComponent<EntityCar>();
            if (hp != null)
            {
                 //hp.GetDamage(damage);          
            }
        }
    }
}
