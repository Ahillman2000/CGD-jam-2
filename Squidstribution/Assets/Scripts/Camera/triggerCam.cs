using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerCam : MonoBehaviour
{
    [SerializeField] private Camera cam1;
    [SerializeField] private Camera cam2;
    
    void Start()
    {
        cam1.enabled = true;
        cam2.enabled = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            cam1.enabled = false;
            cam2.enabled = true;
            StartCoroutine(WaitAfterTrigger(5));
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Charlie is a bitch");
            cam1.enabled = !cam1.enabled;
            cam2.enabled = !cam2.enabled;
        }
    }

    IEnumerator WaitAfterTrigger(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        cam2.enabled = false;
        cam1.enabled = true;
    }
}
