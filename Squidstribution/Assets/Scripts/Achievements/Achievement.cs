using System.Collections.Generic;
using UnityEngine;

public class Achievement : MonoBehaviour
{
    [SerializeField] private GameObject uI;
    private UI uIScript;

    //private Dictionary<string, System.Action> pEvent; 

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
        UI.achievementUnlocked = true;
        uIScript.PopUp("Achievement Unlocked! - 'KILL YOUR FIRST ENEMY'"); // EventManager.TriggerEvent("AchievementUnlocked", achievementText)
        FindObjectOfType<AudioManager>().Play("AchievementPing");  // EventManager.TriggerEvent("AchievementUnlocked");
    }

    private void FiveSoldiersKilled()
    {
        UI.achievementUnlocked = true;
        uIScript.PopUp("Achievement Unlocked! - 'FIVE SOLDIERS KILLED'");
        FindObjectOfType<AudioManager>().Play("AchievementPing");
    }

    private void FirstBuildingDestroyed()
    {
        UI.achievementUnlocked = true;
        uIScript.PopUp("Achievement Unlocked! - 'FIRST BUILDING DESTROYED'");
        FindObjectOfType<AudioManager>().Play("AchievementPing");
    }

    private void FiveBuildingsDestroyed()
    {
        UI.achievementUnlocked = true;
        uIScript.PopUp("Achievement Unlocked! - 'FIVE BUILDINGS DESTROYED'");
        FindObjectOfType<AudioManager>().Play("AchievementPing");
    }
}
