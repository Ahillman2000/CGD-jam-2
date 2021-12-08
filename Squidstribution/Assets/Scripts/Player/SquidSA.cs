using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidSA : MonoBehaviour
{
    private Animator animator;

    [SerializeField] GameObject HitBoxQ;
    [SerializeField] GameObject HitBoxW;
    [SerializeField] GameObject HitBoxE;
    [SerializeField] GameObject ink;
    GameObject inkBlast;

    [SerializeField] float coolDown_q = 6f;
    [SerializeField] float coolDown_w = 10f;
    [SerializeField] float coolDown_e = 7f;
    [SerializeField] float shot_duration = 3.5f;

    public int special_damage;
    private int swipe_damage = 9;
    private int slam_damage = 11;
    private int ink_damage = 1;

    [SerializeField] float hitBoxTimer = 3f;


    // Use these  for the Icon cooldow ( We can gray them out when on cooldown and when they are up they can have color)
    [SerializeField] bool Q_coolDown = false;
    [SerializeField] bool W_coolDown = false;
    [SerializeField] bool E_coolDown = false;
    private bool special_used = false;

    private Squid player;
    private int karmaCost = 30;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Squid>();
    }

    public bool specialMove()
    {
        return special_used;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetKarma() >= karmaCost)
        {
            if (Input.GetKeyDown(KeyCode.Q) && Q_coolDown == false)
            {
                animator.SetTrigger("Attack");
                Q_coolDown = true;
                special_used = true;
                special_damage = slam_damage;
                HitBoxQ.SetActive(true);
                animator.SetInteger("AttackIndex", 0);
                StartCoroutine(CoolDownHitBoxQ());
                StartCoroutine(CoolDownQ());
            }

            /*if (Input.GetKeyDown(KeyCode.W) && W_coolDown == false)
            {
                animator.SetTrigger("Attack");
                W_coolDown = true;
                special_used = true;
                special_damage = swipe_damage;
                HitBoxW.SetActive(true);
                animator.SetInteger("AttackIndex", 1);
                StartCoroutine(CoolDownHitBoxW());
                StartCoroutine(CoolDownW());
            }*/

            if (Input.GetKeyDown(KeyCode.E) && E_coolDown == false)
            {
                animator.SetTrigger("Attack");
                E_coolDown = true;
                special_used = true;
                animator.SetInteger("AttackIndex", 2);
                special_damage = ink_damage;
                //projectile
                Vector3 inkPos = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
                inkBlast = Instantiate(ink, inkPos, transform.rotation);
                inkBlast.transform.parent = player.transform;
                StartCoroutine(CoolDownE());
            }
        }
    }


    IEnumerator CoolDownHitBoxQ()
    {
        yield return new WaitForSecondsRealtime(hitBoxTimer);
        HitBoxQ.SetActive(false);

    }
    IEnumerator CoolDownHitBoxW()
    {
        yield return new WaitForSecondsRealtime(hitBoxTimer);
        HitBoxW.SetActive(false);

    }
    IEnumerator CoolDownQ()
    {
        yield return new WaitForSecondsRealtime(coolDown_q);
        Q_coolDown = false;
        special_used = false;
        player.SetKarma(player.GetKarma() - karmaCost);
    }
    IEnumerator CoolDownW()
    {
        yield return new WaitForSecondsRealtime(coolDown_w);
        W_coolDown = false;
        special_damage = 0;
        special_used = false;
        player.SetKarma(player.GetKarma() - karmaCost);
    }
    IEnumerator CoolDownE()
    {
        yield return new WaitForSecondsRealtime(shot_duration);
        Destroy(inkBlast);
        yield return new WaitForSecondsRealtime(coolDown_e);
        E_coolDown = false;
        special_damage = 0;
        special_used = false;
        player.SetKarma(player.GetKarma() - karmaCost);
    }
}