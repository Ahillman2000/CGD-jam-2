using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage_;

    private void OnCollisionEnter(Collision col)
    {
        /// If the object collided with contains a IDamageable component, deal damage to it
        /// Freeze position + rotation of the squid to prevent unwanted behaviour
        IDamageable hit = col.gameObject.GetComponent<IDamageable>();
        if (hit != null)
        {
            Destroy(gameObject);
            hit.ApplyDamage(damage_);
            Debug.Log(col.gameObject + " took " + damage_ + " damage!");
        }
        else 
        {
            //Destroy(gameObject);
            //Debug.Log(col.gameObject + " does not contain an IDamagable component");
        }
    }

        // Can collide with armature and other unwanted trigger boxes of the squid.
        /*private void OnTriggerEnter(Collider other)
        {
            // If the object collided with contains a IDamageable component, deal damage to it
            IDamageable hit = other.gameObject.GetComponent<IDamageable>();
            if (hit != null)
            {
                Destroy(gameObject);
                hit.ApplyDamage(damage_);
                Debug.Log(other.gameObject + " took " + damage_ + " damage!");
            }
            else 
            {
                Destroy(gameObject);
                Debug.Log(other.gameObject + " does not contain an IDamagable component");
            }
        }*/
}
