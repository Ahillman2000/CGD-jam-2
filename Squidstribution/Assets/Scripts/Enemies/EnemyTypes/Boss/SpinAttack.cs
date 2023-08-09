using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator = default;
    [SerializeField] ParticleSystem Effect;
    ChefBoss self;

    Squid player;

    int DPS = 7;
    float AttackFrequency = 0.09f;

    private void Start()
    {
        player = FindObjectOfType<Squid>();
        self = animator.GetComponent<ChefBoss>();
    }

    public void SliceAbility()
    {
        Effect.Play();

        InvokeRepeating("Damage", 0, AttackFrequency);
    }

    public void StopSlice()
    {
        CancelInvoke();
        Effect.Stop();
        animator.ResetTrigger("SliceAttack");
    }

    private void Damage()
    {
        if (self.in_range)
        {
            player.ApplyUnrestrictedDamage(DPS);
        }
    }
}
