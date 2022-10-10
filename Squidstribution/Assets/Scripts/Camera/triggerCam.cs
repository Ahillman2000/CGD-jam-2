using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerCam : MonoBehaviour
{
    [SerializeField] private Camera cam1;
    [SerializeField] private Camera cam2;
    private UI ui;
    //bool switched = false;

    void Start()
    {
        cam1.enabled = true;
        cam2.enabled = false;
        ui = GameObject.Find("UI").GetComponent<UI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")/* && !switched*/)
        {
            cam1.enabled = false;
            cam2.enabled = true;
            //switched = true;
            StartCoroutine(WaitAfterTrigger(20));
        }
    }

    /*private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log(gameObject.name + ", " + "left");
            switched = false;
        }
        
    }*/

    private void Update()
    {

        if(!cam1.enabled)
        {
            cam1.GetComponent<ClickToScreen>().enabled = false;
        }
        else
        {
            cam1.GetComponent<ClickToScreen>().enabled = true;
        }

        if (!cam2.enabled)
        {
            cam2.GetComponent<ClickToScreen>().enabled = false;
            ui.TurnOffNews();

        }
        else
        {
            cam2.GetComponent<ClickToScreen>().enabled = true;
            ui.TurnOnNews();
        }
    }

    IEnumerator WaitAfterTrigger(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        cam2.enabled = false;
        cam1.enabled = true;
        gameObject.SetActive(false);
    }
}
