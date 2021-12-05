using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage_;

    private void OnCollisionEnter(Collision col)
    {
        /// Freeze position + rotation of the squid to prevent unwanted behaviour
        IDamageable hit = col.gameObject.GetComponent<IDamageable>();
        if (hit != null)
        {
            Destroy(gameObject);
            //Debug.Log(col.gameObject + " took " + damage_ + " damage!");
        }

        if(col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Squid>().ApplyDamage(damage_);
        }
    }
}
