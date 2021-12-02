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
    }
}
