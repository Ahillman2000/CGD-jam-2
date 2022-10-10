using UnityEngine;
using UnityEngine.UI;

public class ChefBoss : Enemy
{
    [SerializeField] private int maxHealth_;
    [SerializeField] private int damage_;
    [SerializeField] private int speed_;
    [HideInInspector] public Transform pathFindTarget;
    [SerializeField] Slider Slider;
    [SerializeField] GameObject boss;

    public override void Start()
    {
        base.Start();
        navMeshAgent.speed = speed_;
        health = maxHealth_;
/*        maxHealth = maxHealth_;*/
        damage = damage_;
        pathFindTarget = FindObjectOfType<Squid>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        base.Attack(pathFindTarget);
        UpdateSlider();
    }

    public override void ApplyDamage(int damage)
    {
        base.ApplyDamage(damage);
    }

    public override void OnCollisionEnter(Collision col)
    {
        base.OnCollisionEnter(col);
    }

    public override void UpdateSlider()
    {
        float currentHealthPCT = (float)health / (float)maxHealth_;
        Slider.value = currentHealthPCT;
    }

    public void ResetAnimator()
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<Animator>().enabled = true;
    }
}
