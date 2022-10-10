using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] GameObject Timeline;

    private void OnCollisionEnter(Collision collision)
    {

        if (gameObject.GetComponent<Building>() == null)
        {
            if (collision.gameObject.tag == "Player")
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        Timeline.SetActive(true);
    }
}
