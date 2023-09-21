using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class triggerCam : MonoBehaviour
{
    [SerializeField] private GameObject followCam;
    [SerializeField] private GameObject newsCam;

    private GameObject brain;
    private UI ui;
    //bool switched = false;

    void Start()
    {
        /*followCam.enabled = true;
        newsCam.enabled = false;*/
        brain = GameObject.Find("GameCamera");
        brain.SetActive(false);
        brain.SetActive(true);
        followCam.GetComponent<ClickToScreen>().enabled = true;
        newsCam.GetComponent<ClickToScreen>().enabled = false;
        ui = GameObject.Find("UI").GetComponent<UI>();
    }

    private void Update()
    {

        if (followCam.GetComponent<CinemachineVirtualCamera>().Priority > 0)
        {
            followCam.GetComponent<ClickToScreen>().enabled = true;
        }
        else
        {
            followCam.GetComponent<ClickToScreen>().enabled = false;
        }

        if (newsCam.GetComponent<CinemachineVirtualCamera>().Priority > 0)
        {
            newsCam.GetComponent<ClickToScreen>().enabled = true;
            ui.TurnOnNews();

        }
        else
        {
            newsCam.GetComponent<ClickToScreen>().enabled = false;
            ui.TurnOffNews();
        }
    }

    private void OnDisable()
    {
        newsCam.GetComponent<CinemachineVirtualCamera>().Priority = 1;
        followCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        Squid player = GameObject.FindGameObjectWithTag("Player").GetComponent<Squid>();

        player.StartCoroutine(player.WaitAfterTrigger(20, newsCam.GetComponent<Camera>(), followCam.GetComponent<Camera>()));
    }
}
