using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement : MonoBehaviour
{
    private void OnEnable()
    {
        //InfantryTroop.infantryKilled += InfantryKilled; // subscribe to the static delegate
    }

    private void OnDisable()
    {
        //InfantryTroop.infantryKilled -= InfantryKilled; // unsubscribe to the static delegate
    }

    private void InfantryKilled()
    {
        Debug.Log("Infantry unit killed");
    }
}
