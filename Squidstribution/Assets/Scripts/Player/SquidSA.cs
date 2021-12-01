using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidSA : MonoBehaviour
{
    private Animator animator;
    
    [SerializeField] float coolDown_q = 6f;
    [SerializeField] float coolDown_w = 10f;
    [SerializeField] float coolDown_e = 7f;

    // Use these  for the Icon cooldow ( We can gray them out when on cooldown and when they are up they can have color)
    [SerializeField] bool Q_coolDown  =  false;
    [SerializeField] bool W_coolDown  =  false;
    [SerializeField] bool E_coolDown  =  false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown(KeyCode.Q) && Q_coolDown == false )
            {
                animator.SetTrigger("Attack");
                Q_coolDown = true;
                animator.SetInteger("AttackIndex", 0);
                StartCoroutine(CoolDownQ());
            }

            if (Input.GetKeyDown(KeyCode.W) && W_coolDown == false)
            {
                animator.SetTrigger("Attack");
                W_coolDown = true;
                animator.SetInteger("AttackIndex", 1);
                StartCoroutine(CoolDownW());
            }

            if (Input.GetKeyDown(KeyCode.E) && E_coolDown == false)
            {
                animator.SetTrigger("Attack");
                E_coolDown = true;
                animator.SetInteger("AttackIndex", 2);
                StartCoroutine(CoolDownE());
            }
    }

    IEnumerator CoolDownQ()
    {
        yield return new WaitForSecondsRealtime(coolDown_q);
        Q_coolDown = false;
        Debug.Log("Charlie is a pussy");
        
    }IEnumerator CoolDownW()
    {
        yield return new WaitForSecondsRealtime(coolDown_w);
        W_coolDown = false;
        Debug.Log("Charlie is a B I T C H");

    }
    IEnumerator CoolDownE()
    {
        yield return new WaitForSecondsRealtime(coolDown_e);
        E_coolDown = false;
        Debug.Log("Charlie is a rabbit");
    }
}
