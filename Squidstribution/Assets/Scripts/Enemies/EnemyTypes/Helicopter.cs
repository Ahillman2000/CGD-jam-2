using UnityEngine;

public class Helicopter : Enemy
{
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
    }

    public override void OnCollisionEnter(Collision col)
    {
        base.OnCollisionEnter(col);
    }
}

