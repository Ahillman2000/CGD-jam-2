using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class KarmaAbilities : MonoBehaviour
{
    public class MyFloatEvent : UnityEvent<float> { }
    public MyFloatEvent OnAbilityUsed = new MyFloatEvent();
    [Header("AbilityInfo")]
    public GameObject Effect;
    //public Image Icon;
    private bool Usable;

    public void TriggerAbility(KarmaAbilities ability)
    {
        if(Usable)
        {
            ability.Ability();
            ability.StartCooldown();
        }
    }

    public abstract void Ability();

    public abstract void StartCooldown();
}
