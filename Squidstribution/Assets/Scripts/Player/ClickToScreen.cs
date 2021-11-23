using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToScreen : MonoBehaviour
{
    [SerializeField] GameObject player;
    private NavMeshAgent agent;
    private Animator anim;

    [SerializeField] float explosionForce = 100;
    [SerializeField] float explosionRadius = 100;
    [SerializeField] float upwardsModifier = 1;

    void Start()
    {
        anim = player.GetComponent<Animator>();
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

                    Vector3 newTargetPos = hit.point;
                    agent.SetDestination(newTargetPos);
                

                // Player can damage this object (intact building ect)
                if (hitObject.CompareTag("Damagable"))
                {
                    //need to organise this, we want him to move to a location on click, but animation stuff should be outside and I need to check if he's at the hitObject to start the animation. Will fix after sleep
                    if (Vector3.Distance(agent.gameObject.transform.position, newTargetPos) == 0)
                    {
                        anim.SetInteger("AttackIndex", Random.Range(0, 3));
                        anim.SetTrigger("Attack");
                        Building buildingScript = hitObject.GetComponent<Building>();
                        buildingScript.TakeDamage(100);
                    }
                    
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

        if(agent.velocity != Vector3.zero)
        {
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
    }
}
