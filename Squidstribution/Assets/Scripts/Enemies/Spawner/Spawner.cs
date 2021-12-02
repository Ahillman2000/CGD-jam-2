using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    private enum EntityType { SOLDIER, TANK, HELICOPTER };

    /*[System.Serializable]
    struct SpawnType
    {
        public EntityType entityType;
        public float spawnDelay;
    }*/

     //private SpawnType spawnType;

    [Header("Entity To Spawn")]
    [SerializeField] private EntityType entityType;
    [SerializeField] private float spawnDelay = 3.5f;

    [Header("Prefabs")]
    [SerializeField] private GameObject soldierPrefab;
    [SerializeField] private GameObject tankPrefab;
    [SerializeField] private GameObject helicopterPrefab;

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerBody;
    [SerializeField] private Transform bulletContainer;

    private List<GameObject> entityList = new List<GameObject>();
    private float timer = 0.0f;

    private void Update()
    {
        if (Time.time > timer)
        {
            if (entityType == EntityType.SOLDIER)
            {
                GameObject soldier = Instantiate(soldierPrefab, transform.position, Quaternion.identity);
                soldier.GetComponent<Soldier>().pathFindTarget = player;
                soldier.GetComponent<Soldier>().GetComponentInChildren<AimTowardsTarget>().player = playerBody;
                soldier.GetComponent<Weapon>().bulletContainer = bulletContainer;
                entityList.Add(soldier);
                soldier.transform.parent = this.transform;
            }

            if (entityType == EntityType.TANK)
            {
                GameObject tank = Instantiate(tankPrefab, transform.position, Quaternion.identity);
                tank.GetComponent<Tank>().pathFindTarget = player;
                tank.GetComponent<Tank>().GetComponentInChildren<AimTowardsTarget>().player = playerBody;
                tank.GetComponent<Weapon>().bulletContainer = bulletContainer;
                entityList.Add(tank);
                tank.transform.parent = this.transform;
            }

            timer = Time.time + spawnDelay;
        }
    }
































    /* TODO
     * Object pooling
     * To be used for enemies, squids, powerups?
     * Subscribe to any threat level change events (Up the spawn frequency + more stronger enemy spawns)?
     */

    /*private enum Type { SQUID, ENEMY, POWERUP };
    [SerializeField] private Type entity;*/



    /* [SerializeField] private GameObject entityPrefab;
     //[SerializeField] private List<GameObject> entityList = new List<GameObject>();

     [SerializeField] private Transform player;
     [SerializeField] private Transform playerBody;
     [SerializeField] private Transform bulletContainer;
     private float timer = 0.0f;
     private float spawnDelay = 10.0f; 

     private void Start()
     {
         //entity.GetComponent<Enemy>().GetComponentInChildren<Soldier>().
         //entityPrefab.GetComponent<Enemy>().GetComponentInChildren<NavMeshAgent>().destination = playerBody.position;
         //entityPrefab.GetComponent<Weapon>().bulletContainer = bulletContainer;
     }

     private void Update()
     {
         if (Time.time > timer)
         {
             GameObject entity = Instantiate(entityPrefab, transform.position, Quaternion.identity);
             entity.transform.position = this.transform.position;

             timer = Time.time + spawnDelay;
         }
     }*/
}