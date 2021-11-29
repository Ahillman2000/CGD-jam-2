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
        if (GetComponent<NavMeshAgent>() != null && GetComponent<NavMeshAgent>().enabled)
        {
            navMeshAgent.destination = target.position;
        } else { Debug.Log(this.gameObject + " does not contain a NavMeshAgent or it is disabled!"); }

        //navMeshAgent.updateRotation = false;
        //navMeshAgent.transform.Rotate(new Vector3(-90, 0, 0));
    }
}
