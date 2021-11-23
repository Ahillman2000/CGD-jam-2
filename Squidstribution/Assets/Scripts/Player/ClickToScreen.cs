using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToScreen : MonoBehaviour
{
    [SerializeField] GameObject player;
    private NavMeshAgent agent;

    [SerializeField] float explosionForce = 100;
    [SerializeField] float explosionRadius = 100;
    [SerializeField] float upwardsModifier = 1;

    void Start()
    {
        agent = player.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //create a ray cast and set it to the mouses cursor position in game
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, float.PositiveInfinity))
            {
                GameObject hitObject = hit.transform.gameObject;
                //Debug.Log(hitObject.tag);

                // Player can move to a space on this object (road etc.)
                if (hitObject.CompareTag("Movable"))
                {
                    Vector3 newTargetPos = hit.point;
                    agent.SetDestination(newTargetPos);
                }

                // Player can damage this object (intact building ect)
                if (hitObject.CompareTag("Damagable"))
                {
                    Building buildingScript = hitObject.GetComponent<Building>();
                    buildingScript.TakeDamage(100);
                }

                // Player can destroy this object (pieces of buildings)
                if (hitObject.CompareTag("Destructable"))
                {
                    Rigidbody hitObjectRB = hitObject.GetComponent<Rigidbody>();
                    hitObjectRB.AddExplosionForce(explosionForce, hit.point, explosionRadius, upwardsModifier, ForceMode.Impulse);
                }

                // player can attack this object (enemy)
                if (hitObject.CompareTag("Attackable"))
                {

                }
            }
        }
    }
}
