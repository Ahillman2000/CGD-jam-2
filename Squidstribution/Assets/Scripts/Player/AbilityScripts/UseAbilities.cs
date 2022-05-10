using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAbilities : MonoBehaviour
{
    KarmaAbilities[] abilities;
    //[SerializeField] SlamAbility AbilityQ;
    //[SerializeField] SpinAbility AbilityW;
    //[SerializeField] InkAbility AbilityE;
    //[SerializeField] ThrowAbility AbilityR;

    private void Start()
    {
        abilities = GetComponents<KarmaAbilities>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            abilities[0].TriggerAbility(SlamAbility.CooldownTime, SlamAbility.cost);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            abilities[1].TriggerAbility(SpinAbility.CooldownTime, SpinAbility.cost);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            abilities[2].TriggerAbility(InkAbility.CooldownTime, InkAbility.cost);
        }

        /*if (Input.GetKeyDown(KeyCode.R))
        {

        }*/

    }
}
