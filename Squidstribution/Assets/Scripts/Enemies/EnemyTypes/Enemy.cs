using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    #region Shared Properties
    protected int health    = 0;
    protected int maxHealth = 0;
    protected int damage    = 0;
    protected int karma     = 10;
    protected NavMeshAgent navMeshAgent; 
    protected Slider slider; 
    #endregion

    private float timer          = 0.0f;
    private const float cooldown = 0.0f;

    private static int killCount;

    public virtual void Start()
    {
        slider = GetComponentInChildren<Slider>(); // null check
        navMeshAgent = GetComponent<NavMeshAgent>(); // null check
    }

    protected virtual void Patrol() {}
    protected virtual void Attack(Transform target)
    {
        if (target != null)
        {
            navMeshAgent.destination = target.position;
        }
    }
    protected virtual void Search() {} 
    
    protected virtual void UpdateSlider() 
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

        if (health <= 0) 
        {
            Destroy(gameObject);
            killCount += 1;
            EventParam eventParam = new EventParam(); 
            eventParam.gameobject_ = this.gameObject;
            eventParam.int_ = karma;
            EventManager.TriggerEvent("EnemyKilled", eventParam);
        };
    }

    public virtual void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Squid>().ApplyDamage(damage);
            //Debug.Log(col.gameObject + " took " + damage + " damage!");
        }
    }
}


