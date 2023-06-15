using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InkAbility : KarmaAbilities
{
    [Header("References")]
    [SerializeField] private Animator animator = default;
    [SerializeField] GameObject Effect;
    [SerializeField] Image Icon;
    GameObject effectCopy;
    GameObject player;

    Break BuildingBreak;
    Enemy EnemyDamage;
    Destructable destructable;

    [SerializeField] int DPS = 18;
    float AttackFrequency = 0.125f;
    [SerializeField] float AttackDuration = 7f;
    float AttackTimer = 0;
    float cooldownTimer = 0;

    private void Start()
    {
        player = animator.gameObject;
        CooldownTime = 10f;
        Cost = 110;
    }
    public override void Ability()
    {
        animator.SetTrigger("Attack");
        animator.SetInteger("AttackIndex", 3);
        StartCoroutine(WaitOnAttack(1.06f));        
    }

    private void Update()
    {
        if (!InCooldown && CalculateKarma.instance.GetKarma() >= Cost)
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
            StopCoroutine("WaitOnAttack");
            cooldownTimer -= Time.deltaTime;
            if (CalculateKarma.instance.GetKarma() >= Cost)
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

        if (other.gameObject.GetComponent<Destructable>() != null)
        {
            destructable = other.gameObject.GetComponent<Destructable>();
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

        if (collision.gameObject.GetComponent<Destructable>() != null)
        {
            destructable = null;
        }
    }
    void DoBuildingDamage()
    {
        if(BuildingBreak != null && BuildingBreak.gameObject.GetComponent<Building>().GetHealth() > 0)
            BuildingBreak.ApplyDamage(DPS);
    }

    void DoEnemyDamage()
    {
        if(EnemyDamage != null && EnemyDamage.gameObject.GetComponent<Enemy>().GetHealth() > 0)
            EnemyDamage.ApplyDamage(DPS);
    }

    void DoDestroyDamage()
    {
        if (destructable != null && destructable.gameObject.GetComponent<Destructable>().GetHealth() > 0)
            destructable.ApplyDamage(DPS);
    }

    IEnumerator WaitOnAttack(float delay) 
    {
        yield return new WaitForSeconds(delay);

        effectCopy = Instantiate(Effect, transform.position, transform.rotation);
        effectCopy.transform.parent = player.transform;
        effectCopy.transform.localScale = new Vector3(player.GetComponent<Squid>().getScale(), player.GetComponent<Squid>().getScale(), player.GetComponent<Squid>().getScale());
        AttackTimer = 0;


        InvokeRepeating("DoBuildingDamage", 0, AttackFrequency);


        InvokeRepeating("DoEnemyDamage", 0, AttackFrequency);

        InvokeRepeating("DoDestroyDamage", 0, AttackFrequency);

        player.GetComponent<Squid>().SetKarma(player.GetComponent<Squid>().GetKarma() - Cost);
        Usable = false;
        Attacking = true;
    }
}
