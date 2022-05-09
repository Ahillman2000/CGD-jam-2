using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkAbility : KarmaAbilities
{
    [Header("References")]
    [SerializeField] private Animator animator = default;
    [SerializeField] private IDamageable damagable = default;
    float CooldownTime = 10f;
    public override void Ability()
    {
        animator.SetTrigger("Attack");
        animator.SetInteger("AttackIndex", 3);
        OnAbilityUsed.Invoke(CooldownTime);
        StartCooldown();
    }

    public override void StartCooldown()
    {
        
    }
}
