using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathNavScript : MonoBehaviour
{
    private NavMeshAgent agent;

    //public Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        //agent.SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                Vector3 newTargetPos = hit.point;
                agent.SetDestination(newTargetPos);
            }
        }
    }
}
