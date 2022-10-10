using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Crappy spawners, will remake soon
/// </summary>
public class Spawner : MonoBehaviour
{
    private enum EntityType { SOLDIER, TANK, HELICOPTER, BABYSQUID };

    [Header("Entity To Spawn")]
    [SerializeField] private EntityType entityType;
    [SerializeField] private float spawnDelay = 3.5f;

    [Header("Prefabs")]
    [SerializeField] private GameObject soldierPrefab;
    [SerializeField] private GameObject tankPrefab;
    [SerializeField] private GameObject helicopterPrefab;
    [SerializeField] private GameObject babySquidPrefab;

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerBody;
    [SerializeField] private Transform bulletContainer;

    private static List<GameObject> entityList = new List<GameObject>();
    private float timer     = 0.0f;
    private int maxEntities = 3;

    private void OnEnable()
    {
        EventManager.StartListening("EnemyKilled", UpdateEntityList);
        EventManager.StartListening("ThreatLevelChange", UpdateSpawnLimit);
    }

    private void OnDisable()
    {
        EventManager.StopListening("EnemyKilled", UpdateEntityList);
        EventManager.StopListening("ThreatLevelChange", UpdateSpawnLimit);
    }

    private void OnApplicationQuit()
    {
        Destroy(this);
        EventManager.StopListening("EnemyKilled", UpdateEntityList);
        EventManager.StopListening("ThreatLevelChange", UpdateSpawnLimit);
    }

    private void Start()
    {
        if (entityType == EntityType.SOLDIER)
        {
            timer = 5.0f;
        }
        else
        {
            timer = 23.5f;
        }
    }

    private void Update()
    {
        if (Time.time > timer)
        {
            if (entityList.Count < maxEntities)
            {
                if (entityType == EntityType.SOLDIER)
                {
                    GameObject soldier = Instantiate(soldierPrefab, transform.position, Quaternion.identity);
                    soldier.GetComponent<Soldier>().pathFindTarget = player;
                    soldier.GetComponent<Soldier>().GetComponentInChildren<AimTowardsTarget>().player = playerBody;
                    soldier.GetComponent<Weapon>().bulletContainer = bulletContainer;
                    entityList.Add(soldier);
                    //soldier.transform.parent = this.transform;
                }

                if (entityType == EntityType.TANK)
                {
                    GameObject tank = Instantiate(tankPrefab, transform.position, Quaternion.identity);
                    tank.GetComponent<Tank>().pathFindTarget = player;
                    tank.GetComponent<Weapon>().bulletContainer = bulletContainer;
                    entityList.Add(tank);
                   // tank.transform.parent = this.transform;
                }

                if (entityType == EntityType.HELICOPTER)
                {
                    GameObject helicopter = Instantiate(helicopterPrefab, transform.position, Quaternion.identity);
                    helicopter.GetComponent<Helicopter>().pathFindTarget = player;
                    entityList.Add(helicopter);
                   // helicopter.transform.parent = this.transform;
                }

                if (entityType == EntityType.BABYSQUID)
                {

                    GameObject babysquid = Instantiate(babySquidPrefab, transform.position, Quaternion.identity);
                    SquidSelect.Instance.SquidList.Add(this.gameObject);
                    babysquid.GetComponent<BabySquid>().pathFindTarget = player; //random enemy or building target
                   // babysquid.transform.parent = this.transform;
                }
            }
            timer = Time.time + spawnDelay;
        }
        //Debug.Log("Spawned entities: " + entityList.Count);
    }

    private void UpdateEntityList(EventParam eventParam)
    {
        entityList.Remove(eventParam.gameobject_);
    }

    private void UpdateSpawnLimit(EventParam threat)
    {
        switch(threat.float_)
        {
            case 1:
                maxEntities = 3;
                break;
            case 2:
                maxEntities = 6;
                break;
            case 3:
                maxEntities = 9;
                break;
            case 4:
                maxEntities = 12;
                break;
        }
    }

    private void OnDestroy()
    {
        SquidSelect.Instance.SquidList.Remove(this.gameObject);
    }
}
