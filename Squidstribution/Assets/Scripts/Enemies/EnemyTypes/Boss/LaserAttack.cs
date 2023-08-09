using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator = default;
    [SerializeField] ParticleSystem Effect;
    ChefBoss self;

    Squid player;

    int DPS = 9;
    float AttackFrequency = 0.2f;

    private void Start()
    {
        player = FindObjectOfType<Squid>();
        self = animator.GetComponent<ChefBoss>();
    }

    public void PlayEffect()
    {
        Effect.Play();
    }

    public void LaserAbility()
    {
        InvokeRepeating("Damage", 0, AttackFrequency);
    }

    public void StopLaser()
    {
        CancelInvoke();
        Effect.Stop();
        animator.ResetTrigger("RangedAttack");
    }

    private void Damage()
    {
        if (self.in_range)
        {
            player.ApplyUnrestrictedDamage(DPS);
        }
    }
}
