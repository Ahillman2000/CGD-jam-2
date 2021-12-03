using UnityEngine;

public class Achievement : MonoBehaviour
{
    private void OnEnable() // subscribe to the static delegates
    {   
        //EventManager.StartListening("FirstKill", FirstKill);
        EventManager.StartListening("FiveSoldiersKilled", FiveSoldiersKilled); 
    }

    private void OnDisable() // unsubscribe to the static delegates
    {
        //EventManager.StopListening("FirstKill", FirstKill);
        EventManager.StopListening("FiveSoldiersKilled", FiveSoldiersKilled);
    }

    private void FirstKill()
    {
        Debug.Log("[Achievement Unlocked]: 'KILL YOUR FIRST ENEMY'");
        // Pop achievement - could simply just alter the text of the pop-up banner for now
    }

    private void FiveSoldiersKilled()
    {
        Debug.Log("[Achievement Unlocked]: 'FIVE SOLDIERS KILLED'");
        EventManager.StopListening("FiveSoldiersKilled", FiveSoldiersKilled); // dirty fix
    }
}
