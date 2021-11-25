using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{

    public GameObject fractured;
    public float breakForce;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("f"))
        {
            BreakThing();
        }
    }

    public void BreakThing()
    {
        GameObject frac = Instantiate(fractured, gameObject.transform.position, gameObject.transform.rotation);

        foreach(Rigidbody rb in frac.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 force = (rb.transform.position - transform.position).normalized * breakForce;
            Debug.Log(force);
            rb.AddExplosionForce(10, frac.transform.position,3);
        }

        Destroy(gameObject);
    }
}
