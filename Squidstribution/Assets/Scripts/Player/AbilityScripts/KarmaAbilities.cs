using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public abstract class KarmaAbilities : MonoBehaviour
{
    public class MyFloatEvent : UnityEvent<float> { }
    public MyFloatEvent OnAbilityUsed = new MyFloatEvent();
    [Header("AbilityInfo")]
    [HideInInspector]public bool Usable = false;
    [HideInInspector]public bool Attacking = false;
    [HideInInspector]public bool InCooldown = false;

    public void TriggerAbility(float AbilityCooldown, int cost)
    {
        if(!Usable && CalculateKarma.instance.GetKarma() >= cost && !Attacking && !InCooldown)
        {
            Usable = true;
        }
        if(Usable && CalculateKarma.instance.GetKarma() >= cost)
        {
            OnAbilityUsed.Invoke(AbilityCooldown);
            Ability();
            //StartCoroutine(StartCooldown(AbilityCooldown));
        }
    }

    public abstract void Ability();
}
