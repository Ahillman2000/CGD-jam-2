using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private Camera cam1;
    [SerializeField] private Camera cam2;

    void Start()
    {
        cam1.enabled = true;
        cam2.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            cam1.enabled = !cam1.enabled;
            cam2.enabled = !cam2.enabled;
        }

        if (!cam1.enabled)
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
        }
        else
        {
            cam2.GetComponent<ClickToScreen>().enabled = true;
        }
    }
}