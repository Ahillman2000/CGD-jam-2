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
            abilities[0].TriggerAbility(abilities[0].CooldownTime, abilities[0].Cost);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            abilities[1].TriggerAbility(abilities[1].CooldownTime, abilities[1].Cost);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            abilities[2].TriggerAbility(abilities[2].CooldownTime, abilities[2].Cost);
        }

        /*if (Input.GetKeyDown(KeyCode.R))
        {

        }*/

    }
}
