using System;
using UnityEngine;

public class Helicopter : Enemy
{
    public static event Action killed;

    [SerializeField] private int maxHealth_;
    [SerializeField] private int damage_;
    [SerializeField] private int speed_;
    public Transform pathFindTarget;

    public override void Start()
    {
        base.Start();
        navMeshAgent.speed = speed_;
        health = maxHealth_;
        maxHealth = maxHealth_;
        damage = damage_;
    }

    private void Update()
    {
        base.Attack(pathFindTarget);
        base.UpdateSlider();
    }

    public override void ApplyDamage(int damage)
    {
        base.ApplyDamage(damage);
        if (health <= 0)
        {
            killed?.Invoke();
        }
    }

    public override void OnCollisionEnter(Collision col)
    {
        base.OnCollisionEnter(col);
    }
}

