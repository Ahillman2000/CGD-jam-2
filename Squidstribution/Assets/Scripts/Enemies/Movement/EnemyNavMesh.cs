using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    [SerializeField] private Transform target;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        if(GetComponent<NavMeshAgent>() != null && GetComponent<NavMeshAgent>().enabled)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        } else { Debug.Log(this.gameObject + " does not contain a NavMeshAgent or it is disabled!"); }
    }

    private void Update()
    {
        if(GetComponent<NavMeshAgent>() != null && target != null)
        {
            navMeshAgent.destination = target.position;

            /// Tank: Go to the players last known position as of 5 seconds ago, whilst always aiming barrel at player with AimAtScript
        }
        else { Debug.Log(this.gameObject + " does not contain a NavMeshAgent or it is disabled!"); }

        //navMeshAgent.updateRotation = false;
        //navMeshAgent.transform.Rotate(new Vector3(-90, 0, 0));
    }
}
