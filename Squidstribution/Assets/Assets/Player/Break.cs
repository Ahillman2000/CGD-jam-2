using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    [SerializeField]
    private GameObject fractured;
    [SerializeField]
    private float breakForce;

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
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        GameObject frac = Instantiate(fractured, gameObject.transform.position, Quaternion.identity);
        

        foreach(Rigidbody rb in frac.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 force = (rb.transform.position - transform.position).normalized * breakForce;
            rb.AddExplosionForce(10, frac.transform.position,3);
        }

        Destroy(gameObject);
    }
}
