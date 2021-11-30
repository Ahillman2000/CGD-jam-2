using System;
using UnityEngine;

public class Soldier : MonoBehaviour/*, IDamageable*//*: NPC*/
{
















    /*public event Action<float> onHealthChange;

    private float health;
    [SerializeField] private float maxHealth;

    private void Start()
    {
        health = maxHealth;
        Debug.Log("Soldier health: " + health);
    }*/

    /*void IDamageable.ApplyDamage(float damage)
    {
        health -= damage;

        *//*float currentHealthPercent = (float)health / (float)maxHealth;
        onHealthChange?.Invoke(currentHealthPercent);

        if (health <= 0)
        {
            Destroy(gameObject);
        }*//*
    }*/

    /*public static event Action infantryKilled;
    public event Action<float> infantryHealthPCTChange;

    #region Properties
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int damage;
    //[SerializeField] private float speed;
    #endregion

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
            infantryKilled?.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            health -= 20;
            float currentHealthPCT = (float)health / (float)maxHealth;
            infantryHealthPCTChange?.Invoke(currentHealthPCT);
        }
    }*/

    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("COLLISION ENTER");
            health -= 10;
            float currentHealthPCT = (float)health / (float)maxHealth;
            infantryHealthPCTChange?.Invoke(currentHealthPCT);
        }
    }*/
}
