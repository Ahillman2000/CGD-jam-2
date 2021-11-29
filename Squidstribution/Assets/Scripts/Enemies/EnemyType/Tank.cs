using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : NPC
{
    #region Properties
    [SerializeField] private int health_ = 0;
    [SerializeField] private int damage_ = 0;
    [SerializeField] private int speed_ = 0;
    private enum State { PATROL, ATTACK, SEARCH };
    [SerializeField] private State current_state = State.PATROL;
    #endregion

    private void Start()
    {
        health = health_;
        damage = damage_;
        speed = speed_;
    }
    public void Update()
    {
        //Debug.Log("Tank Health: " + CurrentHealth);
        // use events instead?
        switch (current_state)
        {
            case State.PATROL:
                Patrol();
                break;
            case State.ATTACK:
                Attack();
                break;
            case State.SEARCH:
                Search();
                break;
        }
    }

    #region Behaviour
    protected override void Patrol() {} 
    protected override void Attack() {} 
    protected override void Search() {}
    #endregion

    protected override void Die()
    {
        // death anim, audio, particle effect
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();
        if (hit != null)
        {
            hit.ApplyDamage(damage); // damages OTHER object not this one
        }
    }
}
