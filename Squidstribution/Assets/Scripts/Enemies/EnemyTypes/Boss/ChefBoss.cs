using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChefBoss : Enemy
{
    [SerializeField] private int maxHealth_;
    //[SerializeField] private int damage_;
    [SerializeField] private int karma_;
    [SerializeField] private float speed_;
    public Transform pathFindTarget;
    [SerializeField] Slider Slider;

    public List<PoweredObject> HealSources;

    /*[SerializeField] ParticleSystem laser;
    [SerializeField] ParticleSystem Spin;*/

    Animator anim;

    [HideInInspector] public bool in_range = false;
    enum attack_states { RANGED, MELEE, RETREAT}

    attack_states current_state = attack_states.MELEE;

    public override void Start()
    {
        base.Start();
        if(anim == null)
        {
            anim = GetComponent<Animator>();
        }
        health = maxHealth_;
        //damage = damage_;
        karma = karma_;
        navMeshAgent.speed = speed_;
        //Spin.Stop();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.P))
        {
            ApplyDamage(maxHealth_);
        }
#endif


        base.Attack(pathFindTarget);
        UpdateSlider();
        //anim.speed = navMeshAgent.speed;
    }

    public override void ApplyDamage(int damage)
    {
        base.ApplyDamage(damage);
        if (damage > 0)
        {
            pathFindTarget.GetComponent<Squid>().ApplyDamage(10);
        }


        if (health <= 0)
        {
            EventManager.TriggerEvent("KilledBoss", new EventParam());
        }

        if (GetHealth() <= (2 * maxHealth_) / 3 && health > maxHealth_ / 2)
        {
            SetPhase(attack_states.RANGED, 30, 25);
        }

        if (GetHealth() <= maxHealth_ / 3 && HealSources.Count > 0)
        {
            SetPhase(attack_states.RETREAT, 100, 10);
        }

        if (GetHealth() > (2 * maxHealth_) / 3)
        {
            SetPhase(attack_states.MELEE, 7, 45);
        }
    }

    public override void UpdateSlider()
    {
        float currentHealthPCT = (float)health / (float)maxHealth_;
        Slider.value = currentHealthPCT;
    }

    public override void OnCollisionEnter(Collision col)
    {
        if(current_state == attack_states.RETREAT)
        {
            return;
        }

        if (col.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Attack");
            in_range = true;
            if (current_state == attack_states.MELEE)
            {
                anim.SetTrigger("SliceAttack");
            }
            if (current_state == attack_states.RANGED)
            {
                anim.SetTrigger("RangedAttack");

            }
        }
        //anim.ResetTrigger("Attack");
        //base.OnCollisionEnter(col);
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            in_range = false;
        }
    }

    void SetPhase(attack_states new_state, float new_speed, float new_range)
    {
        current_state = new_state;
        navMeshAgent.speed = new_speed;
        navMeshAgent.stoppingDistance = new_range;

        if(new_state == attack_states.MELEE)
        {
            GetComponent<CapsuleCollider>().enabled = true;
            GetComponent<BoxCollider>().enabled = false;
            pathFindTarget = FindObjectOfType<Squid>().transform;
        }

        if(new_state == attack_states.RANGED)
        {
            GetComponent<BoxCollider>().enabled = true;
            GetComponent<CapsuleCollider>().enabled = false;
            pathFindTarget = FindObjectOfType<Squid>().transform;
        }

        if(new_state == attack_states.RETREAT)
        {
            GetComponent<BoxCollider>().enabled = true;
            GetComponent<CapsuleCollider>().enabled = true;

            PoweredObject closest = null;
            float minDistance = Mathf.Infinity;
            Vector3 currentPos = transform.position;
            foreach (PoweredObject source in HealSources)
            {
                float dist = Vector3.Distance(source.transform.position, currentPos);
                if(dist < minDistance)
                {
                    closest = source;
                    minDistance = dist;
                }
            }
            pathFindTarget = closest.transform;
        }
    }

    public void DamagePlayer()
    {
        if (in_range)
        {
            pathFindTarget.GetComponent<Squid>().ApplyDamage(damage);
        }
    }

    public int GetMaxHealth()
    {
        return maxHealth_;
    }

    public void SetHealth(int val)
    {
        health = val;
    }
}
