using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Soldier : Enemy
{
    public static event Action killed;

    [SerializeField] private int maxHealth_;
    [SerializeField] private int damage_;
    [SerializeField] private int speed_;
    private Slider slider; // put into healthbar class?
    private NavMeshAgent navMeshAgent; // put into EnemyMove/EnemyNavMesh class? 
    [SerializeField] private Transform pathFindTarget;

    private void Start()
    {
        slider = GetComponentInChildren<Slider>(); // null check
        navMeshAgent = GetComponent<NavMeshAgent>(); // null check
        navMeshAgent.speed = speed_;
        health = maxHealth_;
        damage = damage_;
        speed = speed_;
    }

    private void Update()
    {
        /// If out of range of the player, start attacking smalls squids. If none left, enter wander state.
        if (pathFindTarget != null)
        {
            navMeshAgent.destination = pathFindTarget.position;
        }

        float currentHealthPCT = (float)health / (float)maxHealth_;
        slider.value = currentHealthPCT;
        // change this. BAD. Just for testing.
        slider.transform.rotation = new Quaternion(slider.transform.rotation.x, Camera.main.transform.rotation.y, slider.transform.rotation.z, slider.transform.rotation.w);
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
