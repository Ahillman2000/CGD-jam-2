using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinAbility : KarmaAbilities
{
    [Header("References")]
    [SerializeField] private Animator animator = default;
    [SerializeField] GameObject Effect;
    [SerializeField] Image Icon;
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
    float cooldownTimer = 0;
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
        Usable = false;
        Attacking = true;
    }

    private void Update()
    {
        if (!InCooldown && CalculateKarma.instance.GetKarma() >= cost)
        {
            Icon.fillAmount = 0;
        }
        else
        {
            Icon.fillAmount = 1;
        }

        if (Attacking)
        {
            AttackTimer += Time.deltaTime;
        }

        if (AttackTimer >= AttackDuration)
        {
            AttackTimer = 0;
            Destroy(effectCopy);
            CancelInvoke();
            animator.SetInteger("AttackIndex", 0);
            cooldownTimer = CooldownTime;
            InCooldown = true;
            Attacking = false;
        }


        if (cooldownTimer >= 0 && InCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (CalculateKarma.instance.GetKarma() >= cost)
            {
                Icon.fillAmount = cooldownTimer / CooldownTime;
            }
        }

        if (cooldownTimer < 0)
        {
            InCooldown = false;
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