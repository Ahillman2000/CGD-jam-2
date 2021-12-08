using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidSA_Hitboxes : MonoBehaviour
{
    GameObject player;
    SquidSA squidSA;

    Break buildingBreak;
    Enemy enemy;


    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        squidSA = player.GetComponent<SquidSA>();
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Break>() != null)
        {
            buildingBreak = other.gameObject.GetComponent<Break>();
            if (squidSA.specialMove())
            {
                buildingBreak.ApplyDamage(squidSA.special_damage);
            }
        }

        if (other.gameObject.GetComponent<Enemy>() != null)
        {
            enemy = other.gameObject.GetComponent<Enemy>();
            if(squidSA.specialMove())
            {
                enemy.ApplyDamage(squidSA.special_damage);
            }
        }
        
        
    }
}