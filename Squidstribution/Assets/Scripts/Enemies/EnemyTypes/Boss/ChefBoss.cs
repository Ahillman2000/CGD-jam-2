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
        pathFindTarget.GetComponent<Squid>().ApplyDamage(10);


        if (health <= 0)
        {
            EventManager.TriggerEvent("KilledBoss", new EventParam());
        }

        if (health <= (2 * maxHealth_) / 3 && health > maxHealth_ / 2)
        {
            SetPhase(attack_states.RANGED, 30, 45, 80);
        }

        if (health <= maxHealth_ / 3)
        {
            SetPhase(attack_states.RETREAT, 60, 0, 0);
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

    void SetPhase(attack_states new_state, float new_speed, float new_range, int new_damage)
    {
        current_state = new_state;
        navMeshAgent.speed = new_speed;
        navMeshAgent.stoppingDistance = new_range;
        damage = new_damage;

        if(new_state == attack_states.MELEE)
        {
            GetComponent<CapsuleCollider>().enabled = true;
            GetComponent<BoxCollider>().enabled = false;
        }

        if(new_state == attack_states.RANGED)
        {
            GetComponent<BoxCollider>().enabled = true;
            GetComponent<CapsuleCollider>().enabled = false;
        }

        if(new_state == attack_states.RETREAT)
        {
            //pathtarget = nearest pylon
        }
    }

    public void DamagePlayer()
    {
        if (in_range)
        {
            pathFindTarget.GetComponent<Squid>().ApplyDamage(damage);
        }
    }
}
