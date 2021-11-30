using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement : MonoBehaviour
{
    private void OnEnable()
    {   
        // subscribe to the static delegates
        Enemy.enemyKilled += FirstKill;
        Soldier.killed += SoldierKilled;
    }

    private void OnDisable()
    {
        // unsubscribe to the static delegates
        Enemy.enemyKilled -= FirstKill;
        Soldier.killed -= SoldierKilled;
    }

    private void SoldierKilled()
    {
        Debug.Log("[Achievement Unlocked]: 'First soldier killed'");
    }

    private void FirstKill()
    {
        Debug.Log("[Achievement Unlocked]: 'Kill Your First Enemy'");
        Enemy.enemyKilled -= FirstKill; // unsubscribe here so it only happens the first time?
    }
}
