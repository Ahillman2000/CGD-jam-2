using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ClickToScreen : MonoBehaviour
{
    [SerializeField] GameObject player, ui;
    private NavMeshAgent agent;
    private Animator anim;
    Building buildingScript;
    private Break buildingBreak;
    private UI uiScript;
    private float clipLength = 0;

    [SerializeField] float explosionForce = 100;
    [SerializeField] float explosionRadius = 100;
    [SerializeField] float upwardsModifier = 1;

    void Start()
    {
        anim = player.GetComponent<Animator>();
        agent = player.GetComponent<NavMeshAgent>();
        uiScript = ui.GetComponent<UI>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !uiScript.paused && !uiScript.onMenuButton)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, float.PositiveInfinity))
            {
                GameObject hitObject = hit.transform.gameObject;

                Vector3 newTargetPos = hit.point;
                agent.SetDestination(newTargetPos);
                /// if cannot reach target position then stay at current position

                if (hitObject.GetComponent<Break>().inRange)
                {
                    agent.SetDestination(agent.transform.position);
                    uiScript.SettargetObject(hitObject);
                    anim.SetInteger("AttackIndex", Random.Range(0, 3));
                    anim.SetTrigger("Attack");
                    clipLength = anim.GetCurrentAnimatorStateInfo(0).length;
                    if (hitObject.GetComponent<Break>() != null)
                    { 
                        StartCoroutine(WaitForAnimationToAttack(clipLength, hitObject));                        
                    }
                }
                else if (hitObject.CompareTag("Destructable") && agent.remainingDistance <= 5f)
                {
                    uiScript.SettargetObject(hitObject);
                    Rigidbody hitObjectRB;
                    hitObjectRB = hitObject.GetComponent<Rigidbody>();
                    hitObjectRB.AddExplosionForce(explosionForce, player.transform.position, explosionRadius, upwardsModifier, ForceMode.Impulse);
                }
                else if (hitObject.CompareTag("Nestable"))
                {

                }
            }
        }

        if (agent.velocity != Vector3.zero)
        {
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
            player.GetComponent<Rigidbody>().isKinematic = true;
            player.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    private IEnumerator WaitForAnimationToAttack(float time, GameObject hitObject)
    {
        yield return new WaitForSecondsRealtime(time);
        clipLength = 0;
        buildingBreak = hitObject.GetComponent<Break>();
        buildingBreak.TakeDamage(100);
    }
}


