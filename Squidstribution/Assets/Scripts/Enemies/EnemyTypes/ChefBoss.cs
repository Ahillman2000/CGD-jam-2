using UnityEngine;
using UnityEngine.UI;

public class ChefBoss : Enemy
{
    [SerializeField] private int maxHealth_;
    [SerializeField] private int damage_;
    [SerializeField] private int speed_;
    [SerializeField] private int karma_;
    public Transform pathFindTarget;
    [SerializeField] Slider Slider;

    Animator anim;
    bool canAttack = false;

    enum attackState { MELEE, RANGED};

    attackState state = attackState.MELEE;

    public override void Start()
    {
        base.Start();
        if (GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }
        navMeshAgent.speed = speed_;
        health = maxHealth_;
        damage = damage_;
        karma = karma_;
        pathFindTarget = FindObjectOfType<Squid>().transform;
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

        Attack(pathFindTarget);
        UpdateSlider();

        if (navMeshAgent.velocity != Vector3.zero)
        {
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
    }

    protected override void Attack(Transform target)
    {
        if (target != null)
        {
            navMeshAgent.destination = target.position;
        }

        if (state == attackState.MELEE)
        {
            navMeshAgent.stoppingDistance = 5;
        }
        if (state == attackState.RANGED)
        {
            navMeshAgent.stoppingDistance = 35;
        }

        if (canAttack) //AND IN RANGE
        {
            if (state == attackState.MELEE)
            {
                DoMeleeAttack();
            }
            if (state == attackState.RANGED)
            {
                DoRangedAttack();
            }
        }
    }

    public override void ApplyDamage(int damage)
    {
        base.ApplyDamage(damage);


        if (health <= 0)
        {
            EventManager.TriggerEvent("KilledBoss", new EventParam());
        }
    }

   /* public override void OnCollisionEnter(Collision col)
    {
        base.OnCollisionEnter(col);
    }*/

    public override void UpdateSlider()
    {
        float currentHealthPCT = (float)health / (float)maxHealth_;
        Slider.value = currentHealthPCT;
    }

    void DoMeleeAttack()
    {
        //could potentially reuse the player spin ability here (will have to test to see if can get working without being way too overbalanced)
    }

    void DoRangedAttack()
    {
        //could potentially reuse the player shoot ability here (will have to test to see if can get working without being way too overbalanced)
    }

    public override void OnCollisionEnter(Collision col)
    {
        //base.OnCollisionEnter(col);
    }
}
