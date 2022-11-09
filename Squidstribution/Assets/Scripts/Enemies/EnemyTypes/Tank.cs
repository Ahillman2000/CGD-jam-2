using UnityEngine;

public class Tank : Enemy
{
    [SerializeField] private int maxHealth_;
    [SerializeField] private int damage_;
    [SerializeField] private int speed_;
    [SerializeField] private int karma_;
    public Transform pathFindTarget;

    public override void Start()
    {
        base.Start();
        navMeshAgent.speed = speed_;
        health = maxHealth_;
        maxHealth = maxHealth_;
        damage = damage_;
        karma = karma_;
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
