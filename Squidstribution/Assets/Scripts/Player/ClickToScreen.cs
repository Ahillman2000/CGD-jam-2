using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ClickToScreen : MonoBehaviour
{
    [SerializeField] GameObject player, ui;
    private NavMeshAgent agent;
    private Animator anim;
    private UI uiScript;
    private float clipLength = 0;

    public LayerMask Ground;
    public LayerMask Clickable;
    //public CameraShake cameraShake;

    private bool currentlyAttacking = false;

    public void ClearTarget()
    {
        agent.ResetPath();
    }

    void Start()
    {
        anim = player.GetComponent<Animator>();
        agent = player.GetComponent<NavMeshAgent>();
        uiScript = ui.GetComponent<UI>();
    }

    void Update()
    {
        if (!uiScript.paused && !uiScript.onMenuButton)
        {

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, float.PositiveInfinity, Clickable))
                {
                    GameObject hitObject = hit.transform.gameObject;
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        SquidSelect.Instance.ShiftSelect(hitObject);
                    }
                    else
                    {
                        SquidSelect.Instance.ClickSelect(hitObject);
                    }
                }
                else if (Physics.Raycast(ray, out hit, float.PositiveInfinity, Ground) && !Input.GetKey(KeyCode.LeftControl))
                {
                    GameObject hitObject = hit.transform.gameObject;
                    //Debug.Log(hitObject.name);
                    uiScript.SettargetObject(hitObject);
                    Vector3 newTargetPos = hit.point;
                    agent.SetDestination(newTargetPos);
                    if (!Input.GetKey(KeyCode.LeftShift))
                    {
                        SquidSelect.Instance.DeselectAll();
                    }

                }
                else if (Physics.Raycast(ray, out hit, float.PositiveInfinity, Ground) && Input.GetKey(KeyCode.LeftControl))
                {
                    Transform newTarget = hit.transform;
                    foreach (GameObject baby_agent in SquidSelect.Instance.SquidsSelected)
                    {
                        baby_agent.GetComponent<BabySquid>().SetTarget(newTarget);
                    }
                }
            }
            if (Input.GetMouseButtonDown(1) && !currentlyAttacking)
            {
                currentlyAttacking = true;
                anim.SetTrigger("Attack");
                StartCoroutine(WaitForAnimationToAttack(clipLength));
                //BABY SQUIDS TO BE ABLE TO ATTACK TARGET SET HERE
            }

            if (Input.GetKey(KeyCode.Return))
            {
                foreach (GameObject baby_agent in SquidSelect.Instance.SquidsSelected)
                {
                    baby_agent.GetComponent<BabySquid>().SetTarget(player.transform);
                }
                SquidSelect.Instance.DeselectAll();
            }

            if (agent.velocity != Vector3.zero)
            {
                anim.SetBool("IsMoving", true);
            }
            else
            {
                anim.SetBool("IsMoving", false);
/*                if(cameraShake != null)
                {
                    StartCoroutine(cameraShake.Shake(false, 0.3f));
                }*/
                player.GetComponent<Rigidbody>().isKinematic = true;
                player.GetComponent<Rigidbody>().isKinematic = false;
            }
            if (Time.timeScale < 1)
            {
                Time.timeScale += 0.001f;
            }
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
    }
    private IEnumerator WaitForAnimationToAttack(float time)
    {
        yield return new WaitForSeconds(time/2);
        clipLength = 0;
        EventManager.TriggerEvent("SquidAttackAnimStarted", new EventParam());
        yield return new WaitForSeconds(0.5f);
        EventManager.TriggerEvent("SquidAttackAnimFinished", new EventParam());
        currentlyAttacking = false;
        //EventManager.TriggerEvent("SlowMoActive");
        //setSlowMo(true);
    }

    /*void setSlowMo(bool isSlow)
    {
        if(isSlow)
        {
            Time.timeScale = 0.2f;
            
        }
    }*/
}


