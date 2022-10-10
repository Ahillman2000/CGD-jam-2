using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Health;
    public static int Kills;
    public static float Karma;
    public static int SoldiersKilled;
    public static int TanksDestroyed;
    public static int HelicoptersDestroyed;
    public static int ThreatLevelReached;
    public static int BabySquidsSpawned;
    public static int BuildingsDestroyed;
    public static int DistrictsDestroyed;
    public static int AchievementsEarned;
    public static int IceCreamsEaten;

    private void OnEnable()
    {
        EventManager.StartListening("HealthChange",        UpdateHealth);
        EventManager.StartListening("EnemyKilled",         UpdateKills);
        EventManager.StartListening("KarmaChange",         UpdateKarma);
        EventManager.StartListening("SoldierKilled",       UpdateSoldierKills);
        EventManager.StartListening("TankDestroyed",       UpdateTanksDestroyed);
        EventManager.StartListening("HelicopterDestroyed", UpdateHelicoptersDestroyed);
        EventManager.StartListening("ThreatLevelChange",   UpdateThreatLevelReached);
        EventManager.StartListening("BabySquidSpawned",    UpdateBabySquidsSpawned);
        EventManager.StartListening("BuildingDestroyed",   UpdateBuildingsDestroyed);
        EventManager.StartListening("DistrictDestroyed",   UpdateDistrictsDestroyed);
        EventManager.StartListening("IceCreamEaten",   UpdateIceCreamsEaten);
        EventManager.StartListening("AchievementEarned",   UpdateAchievementsEarned);
    }

    private void OnDisable() 
    {
        EventManager.StopListening("HealthChange",        UpdateHealth);
        EventManager.StopListening("EnemyKilled",         UpdateKills);
        EventManager.StopListening("KarmaChange",         UpdateKarma);
        EventManager.StopListening("SoldierKilled",       UpdateSoldierKills);
        EventManager.StopListening("TankDestroyed",       UpdateTanksDestroyed);
        EventManager.StopListening("HelicopterDestroyed", UpdateHelicoptersDestroyed);
        EventManager.StopListening("ThreatLevelChange",   UpdateThreatLevelReached);
        EventManager.StopListening("BabySquidSpawned",    UpdateBabySquidsSpawned);
        EventManager.StopListening("BuildingDestroyed",   UpdateBuildingsDestroyed);
        EventManager.StopListening("DistrictDestroyed",   UpdateDistrictsDestroyed);
        EventManager.StopListening("IceCreamEaten", UpdateIceCreamsEaten);
        EventManager.StopListening("AchievementEarned",   UpdateAchievementsEarned);
    }

    private void OnApplicationQuit()
    {
        Destroy(this);
        EventManager.StopListening("HealthChange", UpdateHealth);
        EventManager.StopListening("EnemyKilled", UpdateKills);
        EventManager.StopListening("KarmaChange", UpdateKarma);
        EventManager.StopListening("SoldierKilled", UpdateSoldierKills);
        EventManager.StopListening("TankDestroyed", UpdateTanksDestroyed);
        EventManager.StopListening("HelicopterDestroyed", UpdateHelicoptersDestroyed);
        EventManager.StopListening("ThreatLevelChange", UpdateThreatLevelReached);
        EventManager.StopListening("BabySquidSpawned", UpdateBabySquidsSpawned);
        EventManager.StopListening("BuildingDestroyed", UpdateBuildingsDestroyed);
        EventManager.StopListening("IceCreamEaten", UpdateIceCreamsEaten);
        EventManager.StopListening("DistrictDestroyed", UpdateDistrictsDestroyed);
    }

    private void UpdateHealth(EventParam eventParam)
    {
        Health++;
        EventManager.TriggerEvent("StatChange", new EventParam());
    }

    private void UpdateIceCreamsEaten(EventParam eventParam)
    {
        IceCreamsEaten++;
        EventManager.TriggerEvent("StatChange", new EventParam());
    }

    private void UpdateKills(EventParam eventParam)
    {
        Kills++;
        EventManager.TriggerEvent("StatChange", new EventParam());
    }

    private void UpdateKarma(EventParam eventParam)
    {
        //Karma += eventParam.float_;
        //Debug.Log("Karma: " + Karma);
        EventManager.TriggerEvent("StatChange", new EventParam());
    }

    private void UpdateSoldierKills(EventParam eventParam)
    {
        SoldiersKilled++;
        EventManager.TriggerEvent("StatChange", new EventParam());
    }

    private void UpdateTanksDestroyed(EventParam eventParam)
    {
        TanksDestroyed++;
        EventManager.TriggerEvent("StatChange", new EventParam());
    }

    private void UpdateHelicoptersDestroyed(EventParam eventParam)
    {
        HelicoptersDestroyed++;
        EventManager.TriggerEvent("StatChange", new EventParam());
    }

    private void UpdateThreatLevelReached(EventParam eventParam)
    {
        ThreatLevelReached++;
        EventManager.TriggerEvent("StatChange", new EventParam());
    }

    private void UpdateBabySquidsSpawned(EventParam eventParam)
    {
        BabySquidsSpawned++;
        EventManager.TriggerEvent("StatChange", new EventParam());
    }

    private void UpdateBuildingsDestroyed(EventParam eventParam)
    {
        BuildingsDestroyed++;
        EventManager.TriggerEvent("StatChange", new EventParam());
    }

    private void UpdateDistrictsDestroyed(EventParam eventParam)
    {
        DistrictsDestroyed++;
        EventManager.TriggerEvent("StatChange", new EventParam());
    }

    private void UpdateAchievementsEarned(EventParam eventParam)
    {
        AchievementsEarned++;
        EventManager.TriggerEvent("StatChange", new EventParam());
    }
}
