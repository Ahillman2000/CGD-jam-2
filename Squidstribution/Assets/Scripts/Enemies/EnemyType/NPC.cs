using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour, IDamageable
{
    #region Shared Properties
    protected float health = 0;
    protected float damage = 0;
    protected float speed = 0.0f;
    #endregion

    private float timer = 0.0f;
    private const float cooldown = 0.0f;

    #region Shared Behaviour
    protected virtual void Patrol() {} // Wander about randomly in an area
    protected virtual void Attack() {} // Attack the player if in range
    protected virtual void Search() {} // Search for the player if they go out of range
    #endregion

    #region IDamageable
    public float CurrentHealth { get { return health; } set { health = value; } }
    public virtual void ApplyDamage(float atk_damage) 
    {
        if(Time.time > timer)
        {
            timer = Time.time + cooldown;
            CurrentHealth -= atk_damage;
        }

        if (CurrentHealth < 1) { Die(); };
    }
    #endregion

    protected abstract void Die();
}
