using System.Collections.Generic;
using UnityEngine;

public class Achievement : MonoBehaviour
{
    public enum ID
    {
        FIRST_KILL,
        FIVE_SOLDIERS_KILLED,
        FIRST_BUILDING_DESTROYED,
        FIVE_BUILDINGS_DESTROYED,
        FIRST_BABYSQUID_SPAWNED,
        BABYSQUID_ARMY,
        EAT_AN_ICE_CREAM,
        HAVE_A_FEAST,
        STEPPED_ON_THE_BEACH,
        DEFEATED_BOSS,
    }
    private List<bool> unlocked = new List<bool>();

    private void Awake()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(ID)).Length; i++)
        {
            unlocked.Add(new bool());
            unlocked[i] = false;
        }
    }

    private void OnEnable()
    {
        EventManager.StartListening("StatChange", CheckConditions);
    }

    private void OnDisable()
    {
        EventManager.StopListening("StatChange", CheckConditions);
    }

    private void OnApplicationQuit()
    {
        Destroy(this);
        EventManager.StopListening("StatChange", CheckConditions);
    }

    private void CheckConditions(EventParam eventParam)
    {
        if (PlayerStats.Kills == 1 && !unlocked[0])
        {
            Unlock(ID.FIRST_KILL);
        }
        if (PlayerStats.SoldiersKilled == 5 && !unlocked[1])
        {
            Unlock(ID.FIVE_SOLDIERS_KILLED);
        }
        if (PlayerStats.BuildingsDestroyed == 1 && !unlocked[2])
        {
            Unlock(ID.FIRST_BUILDING_DESTROYED);
        }
        if (PlayerStats.BuildingsDestroyed == 5 && !unlocked[3])
        {
            Unlock(ID.FIVE_BUILDINGS_DESTROYED);
        }
        if (PlayerStats.BabySquidsSpawned == 1 && !unlocked[4])
        {
            Unlock(ID.FIRST_BABYSQUID_SPAWNED);
        }
        if (PlayerStats.BabySquidsSpawned == 5 && !unlocked[5])
        {
            Unlock(ID.BABYSQUID_ARMY);
        }
        if(PlayerStats.IceCreamsEaten == 1 && !unlocked[6])
        {
            Unlock(ID.EAT_AN_ICE_CREAM);
        }
        if(PlayerStats.IceCreamsEaten == 5 && !unlocked[7])
        {
            Unlock(ID.HAVE_A_FEAST);
        }
        if(PlayerStats.SteppedOnTheBeach == true && !unlocked[8])
        {
            Unlock(ID.STEPPED_ON_THE_BEACH);
        }
        if(PlayerStats.BossKilled == true && !unlocked[9])
        {
            Unlock(ID.DEFEATED_BOSS);
        }
    }

    private void Unlock(ID id)
    {
        string text = "";
        switch(id)
        {
            case ID.FIRST_KILL:
                text = "KILL YOUR FIRST ENEMY";
                unlocked[0] = true;
                break;
            case ID.FIVE_SOLDIERS_KILLED:
                text = "KILL FIVE SOLDIERS";
                unlocked[1] = true;
                break;
            case ID.FIRST_BUILDING_DESTROYED:
                text = "DESTROY YOUR FIRST BUILDING";
                unlocked[2] = true;
                break;
            case ID.FIVE_BUILDINGS_DESTROYED:
                text = "DESTROY FIVE BUILDINGS";
                unlocked[3] = true;
                break;
            case ID.FIRST_BABYSQUID_SPAWNED:
                text = "SPAWN YOUR FIRST BABYSQUID";
                unlocked[4] = true;
                break;
            case ID.BABYSQUID_ARMY:
                text = "BABY SQUID GANG";
                unlocked[5] = true;
                break;
            case ID.EAT_AN_ICE_CREAM:
                text = "YOU ATE AN ICE CREAM";
                unlocked[6] = true;
                break;
            case ID.HAVE_A_FEAST:
                    text = "YOU ATE MANY ICE CREAMS";
                unlocked[7] = true;
                break;
            case ID.STEPPED_ON_THE_BEACH:
                text = "STEPPED ONTO THE SECRET BEACH";
                unlocked[8] = true;
                break;
            case ID.DEFEATED_BOSS:
                text = "KILLED THE SUSHI CHEF ROBOT";
                unlocked[9] = true;
                break;
        }

        EventParam eventParam = new EventParam(); 
        eventParam.soundstr_ = "AchievementPing";
        eventParam.achievementstr_ = "Achievement Unlocked! '" + text + "'";
        EventManager.TriggerEvent("AchievementEarned", eventParam); 
    }
}

