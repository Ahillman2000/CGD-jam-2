using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    #region Shared Properties
    protected int health    = 0;
    protected int maxHealth = 0;
    protected int damage    = 0;
    protected NavMeshAgent navMeshAgent; // put into EnemyMove/EnemyNavMesh class? 
    protected Slider slider; // put into healthbar class?
    #endregion

    private float timer          = 0.0f;
    private const float cooldown = 1.0f;

    private static int killCount;

    public virtual void Start()
    {
        slider = GetComponentInChildren<Slider>(); // null check
        navMeshAgent = GetComponent<NavMeshAgent>(); // null check
    }

    protected virtual void Patrol() {} // Wander about randomly in an area
    protected virtual void Attack(Transform target)
    {
        /// If out of range of the player, start attacking smalls squids. If none left, enter wander state.
        if (target != null)
        {
            navMeshAgent.destination = target.position;
        }
    }
    protected virtual void Search() {} // Search for the player if they go out of range
    
    protected virtual void UpdateSlider() // HealthBar class
    {
        float currentHealthPCT = (float)health / (float)maxHealth;
        slider.value = currentHealthPCT;
        // change this. BAD. Just for testing.
        slider.transform.rotation = new Quaternion(slider.transform.rotation.x, Camera.main.transform.rotation.y, slider.transform.rotation.z, slider.transform.rotation.w);
    }

    public virtual void ApplyDamage(int damage)
    {
        /// Restrict how much damage an enemy can recieve at once
        if (Time.time > timer)
        {
            timer = Time.time + cooldown;
            health -= damage;
        }

        // Separate function? 
        /* if (health <= 0) 
        {
            Destroy(gameObject);
            killCount += 1;
            
            if(killCount == 1)
            {
                EventManager.TriggerEvent("FirstKill");
            }
        };*/
    }

    public virtual void OnCollisionEnter(Collision col)
    {
        // [ISSUE?]
        // Could cause issues later on as enemies can hurt each other. So when then are more
        // enemies around it will be much more of an issue. Change this to player tag? Squid
        // can keep IDamageable though so it can hurt any enemy it wants.

        /// If the object collided with contains a IDamageable component, deal damage to it
        IDamageable hit = col.gameObject.GetComponent<IDamageable>(); 
        if (hit != null)
        {
            hit.ApplyDamage(damage);
            //Debug.Log(col.gameObject + " took " + damage + " damage!");
        }
    }
}


