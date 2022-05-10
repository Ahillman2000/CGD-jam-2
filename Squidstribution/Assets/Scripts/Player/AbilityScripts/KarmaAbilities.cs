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
    //public Image Icon;
    private bool Usable = true;

    public void TriggerAbility(float AbilityCooldown, int cost)
    {
        if(Usable && CalculateKarma.instance.GetKarma() >= cost)
        {
            OnAbilityUsed.Invoke(AbilityCooldown);
            Ability();
            StartCoroutine(StartCooldown(AbilityCooldown));
        }
    }

    public abstract void Ability();

    public IEnumerator StartCooldown(float currentCooldown)
    {
        Usable = false;
        yield return new WaitForSeconds(currentCooldown);
        Usable = true;
    }
}
