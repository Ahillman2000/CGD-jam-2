using UnityEngine;
using UnityEngine.UI;

public class ChefBoss : Enemy
{
    [SerializeField] private int maxHealth_;
    [SerializeField] private int damage_;
    [SerializeField] private int speed_;
    [SerializeField] private int karma_;
    [HideInInspector] public Transform pathFindTarget;
    [SerializeField] Slider Slider;

    Animator anim;

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

        base.Attack(pathFindTarget);
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

    public override void ApplyDamage(int damage)
    {
        base.ApplyDamage(damage);
    }

    public override void OnCollisionEnter(Collision col)
    {
        base.OnCollisionEnter(col);
    }

    public override void UpdateSlider()
    {
        float currentHealthPCT = (float)health / (float)maxHealth_;
        Slider.value = currentHealthPCT;
    }

    void DoMeleeAttack()
    {

    }

    void DoRangedAttack()
    {

    }
}
