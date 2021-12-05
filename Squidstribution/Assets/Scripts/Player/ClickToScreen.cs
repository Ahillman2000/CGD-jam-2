﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ClickToScreen : MonoBehaviour
{
    [SerializeField] GameObject player, ui;
    private NavMeshAgent agent;
    private Animator anim;
    private Break buildingBreak;
    private UI uiScript;
    private float clipLength = 0;

    private bool currentlyAttacking = false;

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
                Debug.Log(hitObject.name);
                uiScript.SettargetObject(hitObject);
                Vector3 newTargetPos = hit.point;
                agent.SetDestination(newTargetPos);
            }
        }
        if (Input.GetMouseButtonDown(1) && !currentlyAttacking)
        {
            currentlyAttacking = true;
            //change context button to right click???
            anim.SetInteger("AttackIndex", 1/*Random.Range(0, 3)*/); /// 1 = sweep attack 
            anim.SetTrigger("Attack");
            clipLength = anim.GetCurrentAnimatorStateInfo(0).length / 6 /*anim.GetCurrentAnimatorStateInfo(0).speed*/;  /// 6 is the animation speed of sweep attack 
            StartCoroutine(WaitForAnimationToAttack(clipLength));
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
        if(Time.timeScale < 1)
        {
            Time.timeScale += 0.001f;
        }
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
    private IEnumerator WaitForAnimationToAttack(float time)
    {
        yield return new WaitForSeconds(time);
        clipLength = 0;
        EventManager.TriggerEvent("SquidAttackAnimFinished");
        yield return new WaitForSeconds(0.5f);
        currentlyAttacking = false;
        //EventManager.TriggerEvent("SlowMoActive");
        //setSlowMo(true);
    }

    void setSlowMo(bool isSlow)
    {
        if(isSlow)
        {
            Time.timeScale = 0.2f;
            
        }
    }
}


