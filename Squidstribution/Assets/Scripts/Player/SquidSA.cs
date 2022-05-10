using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquidSA : MonoBehaviour
{
    private Animator animator;

    [SerializeField] GameObject HitBoxQ;
    [SerializeField] GameObject HitBoxW;
    [SerializeField] GameObject AbilityE;
    GameObject inkBlast;
    
    float CoolDownTimer;
    [SerializeField] float AbilityQCoolTime = 4f;
    [SerializeField] float AbilityWCoolTime = 7f;
    [SerializeField] float AbilityECoolDown = 10f;
    //[SerializeField] float AbilityRCoolDown = 7f;
    [SerializeField] float shot_duration = 3.5f;

    [HideInInspector]public int special_damage;
    private int swipe_damage = 1;
    private int slam_damage = 11;
    private int ink_damage = 1;

    [SerializeField] float hitBoxTimer = 3f;

    /// <summary>
    /// NEED TO REWORK TIMINGS TO WORK USING DELTA TIME INSTEAD
    /// ALSO MAKE ALL HIT BOXES BASED ON PREFABS LIKE THE INK ATTACK (CAN STORE TIMINGS, DAMAGE, COOLDOWN, KARMA COST ETC
    /// IN THERE) AND CAN MAKE GOOD LOOKING PARTICLES FOR THE MOVES AS WELL
    /// </summary>

    // Use these  for the Icon cooldow ( We can gray them out when on cooldown and when they are up they can have color)
    [SerializeField] bool Q_coolDown = false;
    [SerializeField] bool W_coolDown = false;
    [SerializeField] bool E_coolDown = false;

    [SerializeField] Image Q_CoolDownBox;
    [SerializeField] Image W_CoolDownBox;
    [SerializeField] Image E_CoolDownBox;
    private bool special_used = false;

    private Squid player;
    private int karmaCost = 30;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Squid>();
        Q_CoolDownBox.fillAmount = 0.0f;
        W_CoolDownBox.fillAmount = 0.0f;
        E_CoolDownBox.fillAmount = 0.0f;
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
                animator.SetInteger("AttackIndex", 1);
                StartCoroutine(CoolDownHitBoxQ());
                StartCoroutine(CoolDownQ());
                Q_CoolDownBox.fillAmount = 1f;
            }

            if (Input.GetKeyDown(KeyCode.W) && W_coolDown == false)
            {
                animator.SetTrigger("Attack");
                W_coolDown = true;
                special_used = true;
                special_damage = swipe_damage;
                HitBoxW.SetActive(true);
                animator.SetInteger("AttackIndex", 2);
                StartCoroutine(CoolDownHitBoxW());
                StartCoroutine(CoolDownW()); 
                W_CoolDownBox.fillAmount = 1f;
            }

            if (Input.GetKeyDown(KeyCode.E) && E_coolDown == false)
            {
                E_CoolDownBox.fillAmount = 1f;
                animator.SetTrigger("Attack");
                E_coolDown = true;
                special_used = true;
                animator.SetInteger("AttackIndex", 3);
                special_damage = ink_damage;
                //projectile
                Vector3 inkPos = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
                inkBlast = Instantiate(AbilityE, inkPos, transform.rotation);
                inkBlast.transform.parent = player.transform;
                StartCoroutine(CoolDownE());
            }
        }
    }


    IEnumerator CoolDownHitBoxQ()
    {
        yield return new WaitForSecondsRealtime(hitBoxTimer);
        HitBoxQ.SetActive(false);
        Q_CoolDownBox.fillAmount = 0.0f;
    }
    IEnumerator CoolDownHitBoxW()
    {
        yield return new WaitForSecondsRealtime(hitBoxTimer);
        HitBoxW.SetActive(false);
        W_CoolDownBox.fillAmount = 0.0f;
    }
    IEnumerator CoolDownQ()
    {
        yield return new WaitForSecondsRealtime(AbilityQCoolTime);
        Q_coolDown = false;
        special_used = false;
        player.SetKarma(player.GetKarma() - karmaCost);
    }
    IEnumerator CoolDownW()
    {
        yield return new WaitForSecondsRealtime(AbilityWCoolTime);
        W_coolDown = false;
        special_damage = 0;
        special_used = false;
        player.SetKarma(player.GetKarma() - karmaCost);
    }
    IEnumerator CoolDownE()
    {
        yield return new WaitForSecondsRealtime(shot_duration);
        Destroy(inkBlast);
        special_used = false;
        yield return new WaitForSecondsRealtime(AbilityECoolDown);
        E_coolDown = false;
        special_damage = 0;
        E_CoolDownBox.fillAmount = 0.0f;
        player.SetKarma(player.GetKarma() - karmaCost);
    }
}