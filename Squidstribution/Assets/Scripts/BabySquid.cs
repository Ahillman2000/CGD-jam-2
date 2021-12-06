using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BabySquid : MonoBehaviour/*, IDamageable*/
{
    private int health;
    [SerializeField] private int maxHealth_;
    [SerializeField] private int damage_;
    [SerializeField] private int speed_;

    private NavMeshAgent navMeshAgent;
    public Transform pathFindTarget;
    private Slider slider;

    private void Start()
    {
        slider = GetComponentInChildren<Slider>(); 
        navMeshAgent = GetComponent<NavMeshAgent>(); 

        navMeshAgent.speed = speed_;
        health = maxHealth_;
    }

    private void Update()
    {
        if (pathFindTarget != null)
        {
            navMeshAgent.destination = pathFindTarget.position;
        }

        UpdateSlider();
    }

    public void OnCollisionEnter(Collision col)
    {
        
    }

    /*public override void ApplyDamage(int damage)
    {
        base.ApplyDamage(damage);
        if (health <= 0)
        {
            killCount += 1;
            Destroy(gameObject);

            if (killCount == 5)
            {
                EventManager.TriggerEvent("FiveSoldiersKilled");
            }
        }
    }*/

    private void UpdateSlider()
    {
        float currentHealthPCT = (float)health / (float)maxHealth_;
        slider.value = currentHealthPCT;
        // change this. BAD. Just for testing.
        slider.transform.rotation = new Quaternion(slider.transform.rotation.x, Camera.main.transform.rotation.y, slider.transform.rotation.z, slider.transform.rotation.w);
    }
}
