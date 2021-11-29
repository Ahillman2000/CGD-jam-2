using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilian :  NPC
{
    #region Properties
    [SerializeField] private int health_ = 0;
    [SerializeField] private int damage_ = 0;
    [SerializeField] private int speed_ = 0;
    #endregion

    private void Start()
    {
        health = health_;
        damage = damage_;
        speed = speed_;
    }

    private void Update()
    {
        //Debug.Log("Civilian Health: " + CurrentHealth);
    }

    protected override void Die() 
    {
        // death anim, audio, particle effect
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();
        if(hit != null)
        {
            hit.ApplyDamage(damage); // damages OTHER object not this one
        }
    }
}
