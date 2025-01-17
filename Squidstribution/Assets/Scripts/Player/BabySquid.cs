using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BabySquid : MonoBehaviour/*, IDamageable*/
{
    public Transform pathFindTarget;

    private Transform initialTarget;
    private NavMeshAgent navMeshAgent;
    private Slider slider;
    private int health;
    [SerializeField] private int maxHealth_;
    [SerializeField] private int damage_ = 5;
    [SerializeField] private int speed_;
    private float timer = 0.0f;
    private float delay = 2.0f;

    private void Start()
    {
        slider = GetComponentInChildren<Slider>(); 
        navMeshAgent = GetComponent<NavMeshAgent>(); 

        navMeshAgent.speed = speed_;
        health = maxHealth_;
        initialTarget = pathFindTarget;
    }

    public void SetTarget(Transform new_target)
    {
        pathFindTarget = new_target;
    }

    private void Update()
    {
        if(pathFindTarget != null)
        {
            navMeshAgent.SetDestination(pathFindTarget.position);
        }
        else
        { 
            navMeshAgent.destination = initialTarget.position; 
        }
        UpdateSlider();
    }

    public void OnTriggerStay(Collider other) /// need to tidy this up 
    {
        IDamageable hit = other.gameObject.GetComponent<IDamageable>();
        if(hit != null)
        {
            if(!other.CompareTag("Player") && !other.CompareTag("SquidBuilding")) // temporary
            {
                SetNewTarget(other.transform);
                if (Time.time > timer)
                {
                    hit.ApplyDamage(damage_);
                    timer = Time.time + delay;
                }
            }
        }
    }

    /*public void ApplyDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }*/

    private void UpdateSlider()
    {
        float currentHealthPCT = (float)health / (float)maxHealth_;
        slider.value = currentHealthPCT;
        // change this. BAD. Just for testing.
        slider.transform.rotation = new Quaternion(slider.transform.rotation.x, Camera.main.transform.rotation.y, slider.transform.rotation.z, slider.transform.rotation.w);
    }

    private void SetNewTarget(Transform target)
    {
        pathFindTarget = target;
    }
}
