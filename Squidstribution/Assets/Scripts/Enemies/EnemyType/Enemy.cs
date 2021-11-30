using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    public static event Action enemyKilled;

    protected int health    = 0;
    protected int damage    = 0;
    protected float speed   = 0.0f;

    public virtual void ApplyDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            enemyKilled?.Invoke();
        }
    }

    public virtual void OnCollisionEnter(Collision col)
    {
        // [ISSUE]
        // Could cause issues later on as enemies can hurt each other. So when then are more
        // enemies around it will be much more of an issue. Change this to player tag? Squid
        // can keep IDamageable though so it can hurt any enemy it wants.

        /// If the object collided with contains a IDamageable component, deal damage to it
        IDamageable hit = col.gameObject.GetComponent<IDamageable>(); 
        if (hit != null)
        {
            hit.ApplyDamage(damage);
            Debug.Log(col.gameObject + " took " + damage + " damage!");
        }
    }
}


