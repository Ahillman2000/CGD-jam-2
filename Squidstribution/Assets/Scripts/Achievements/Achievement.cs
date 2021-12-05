using UnityEngine;

public class Achievement : MonoBehaviour
{
    [SerializeField] private GameObject uI;
    private UI uIScript;

    private void Start()
    {
        uIScript = uI.GetComponent<UI>();
    }

    private void OnEnable() // subscribe to the static delegates
    {   
        EventManager.StartListening("FirstKill", FirstKill);
        EventManager.StartListening("FiveSoldiersKilled", FiveSoldiersKilled); 
        EventManager.StartListening("FirstBuildingDestroyed", FirstBuildingDestroyed); 
        EventManager.StartListening("FiveBuildingsDestroyed", FiveBuildingsDestroyed); 
    }

    private void OnDisable() // unsubscribe to the static delegates
    {
        EventManager.StopListening("FirstKill", FirstKill);
        EventManager.StopListening("FiveSoldiersKilled", FiveSoldiersKilled);
        EventManager.StopListening("FirstBuildingDestroyed", FirstBuildingDestroyed);
        EventManager.StopListening("FiveBuildingsDestroyed", FiveBuildingsDestroyed);
    }

    private void FirstKill()
    {
        uIScript.PopUp("[Achievement Unlocked]: 'KILL YOUR FIRST ENEMY'");
    }

    private void FiveSoldiersKilled()
    {
        uIScript.PopUp("[Achievement Unlocked]: 'FIVE SOLDIERS KILLED'");
    }

    private void FirstBuildingDestroyed()
    {
        uIScript.PopUp("[Achievement Unlocked]: 'FIRST BUILDING DESTROYED'");
    }

    private void FiveBuildingsDestroyed()
    {
        uIScript.PopUp("[Achievement Unlocked]: 'FIVE BUILDINGS DESTROYED'");
    }
}
