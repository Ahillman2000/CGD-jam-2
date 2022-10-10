using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    bool healing = false;
    [SerializeField]GameObject contactObj;
    float healTimer = 10f;
    float resetTimer = .5f;
    ParticleSystem effects;

    private void Update()
    {

        if (!healing)
        {
            if (healTimer < 10f)
            {
                resetTimer -= Time.deltaTime;

                if (resetTimer <= 0)
                {
                    healTimer = 10f;
                    resetTimer = .5f;
                }
            }
        }

        if (healing)
        {

                healTimer -= Time.deltaTime;

                if (healTimer <= 0)
                {
                    if (contactObj.GetComponent<Squid>().GetHealth() < contactObj.GetComponent<Squid>().GetMaxHealth())
                    {
                        contactObj.GetComponent<Squid>().ApplyDamage(-15);
                        healTimer = 10f;
                    }
                }
            
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Squid>() != null)
        {
            healing = true;
            contactObj = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Squid>() != null)
        {
            healing = false;
            contactObj = null;
        }
    }
}
