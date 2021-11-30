using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement : MonoBehaviour
{
    private void OnEnable()
    {
        // Soldier.killed += InfantryKilled;  // subscribe to the static delegate
    }

    private void OnDisable()
    {
        // Soldier.killed -= InfantryKilled; // unsubscribe to the static delegate
    }

    private void InfantryKilled()
    {
        Debug.Log("[Achievement Unlocked]: 'First enemy killed'");
    }
}
