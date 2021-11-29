using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    /* TODO
     * Object pooling
     * To be used for enemies, squids, powerups?
     * Subscribe to any threat level change events (Up the spawn frequency + more stronger enemy spawns)?
     */



    /*private enum Type { SQUID, ENEMY, POWERUP };
    [SerializeField] private Type entity;*/

    [SerializeField] private List<GameObject> entity = new List<GameObject>();
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
}
