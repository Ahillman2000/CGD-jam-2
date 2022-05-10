using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAbility : KarmaAbilities
{
    [Header("References")]
    [SerializeField] private Animator animator = default;
    [SerializeField] GameObject Effect;
    GameObject effectCopy;
    GameObject player;

    Break BuildingBreak;
    Enemy EnemyDamage;
    int Damage = 15;
    
    float AttackFrequency = 0.45f;
    float AttackDuration = 6f;
    float AttackTimer = 0;
    [HideInInspector] public static float CooldownTime = 5f;
    [HideInInspector] public static int cost = 50;

    private void Start()
    {
        player = animator.gameObject;
    }
    public override void Ability()
    {
        animator.SetTrigger("Attack");
        animator.SetInteger("AttackIndex", 2);
        effectCopy = Instantiate(Effect, transform.position, transform.rotation);
        effectCopy.transform.parent = player.transform;
        AttackTimer = 0;

        InvokeRepeating("DoBuildingDamage", 0, AttackFrequency);


        InvokeRepeating("DoEnemyDamage", 0, AttackFrequency);

        player.GetComponent<Squid>().SetKarma(player.GetComponent<Squid>().GetKarma() - cost);
    }

    private void Update()
    {

        AttackTimer += Time.deltaTime;

        if (AttackTimer >= AttackDuration)
        {
            AttackTimer = 0;
            Destroy(effectCopy);
            CancelInvoke();
            animator.SetInteger("AttackIndex", 0);
        }

    }

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.GetComponent<Break>() != null)
        {
            BuildingBreak = other.gameObject.GetComponent<Break>();
        }

        if (other.gameObject.GetComponent<Enemy>() != null)
        {
            EnemyDamage = other.gameObject.GetComponent<Enemy>();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Break>() != null)
        {
            BuildingBreak = null;
        }

        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            EnemyDamage = null;
        }
    }
    void DoBuildingDamage()
    {
        if (BuildingBreak != null)
            BuildingBreak.ApplyDamage(Damage);
    }

    void DoEnemyDamage()
    {
        if (EnemyDamage != null)
            EnemyDamage.ApplyDamage(Damage);
    }
}