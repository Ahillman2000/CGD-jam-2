using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamAbility : KarmaAbilities
{
    [Header("References")]
    [SerializeField] private Animator animator = default;
    [SerializeField] GameObject Effect;
    GameObject effectCopy;
    GameObject player;

    Break BuildingBreak;
    Enemy EnemyDamage;
    int Damage = 150;
    float AttackTimer = 0;
    float AttackDuration = 2.9f;
    [HideInInspector] public static float CooldownTime = 3f;
    [HideInInspector] public static int cost = 30;

    private void Start()
    {
        player = animator.gameObject;
    }
    public override void Ability()
    {
        animator.SetTrigger("Attack");
        animator.SetInteger("AttackIndex", 1);
        AttackTimer = 0;

        Invoke("DoBuildingDamage", AttackDuration);


        Invoke("DoEnemyDamage", AttackDuration);

        Invoke("PlayEffect", AttackDuration);

        Invoke("CancelEffect", AttackDuration + 2);

        player.GetComponent<Squid>().SetKarma(player.GetComponent<Squid>().GetKarma() - cost);

    }

    private void Update()
    {

        AttackTimer += Time.deltaTime;

        if (AttackTimer >= AttackDuration)
        {
            AttackTimer = 0;
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

    void PlayEffect()
    {
        effectCopy = Instantiate(Effect, transform.position, transform.rotation);
        effectCopy.transform.parent = player.transform;
    }
    
    void CancelEffect()
    {
        Destroy(effectCopy);
    }
}