using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    [SerializeField]
    private GameObject fractured;
    [SerializeField]
    private float breakForce;
    private float height = 16.2f;

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            BreakThing();
        }
    }

    private void OnTriggerExit()
    {
        BreakThing();
    }

    public void BreakThing()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - height, transform.position.z);
        GameObject frac = Instantiate(fractured, gameObject.transform.position, Quaternion.identity);
        

        foreach(Rigidbody rb in frac.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 force = (rb.transform.position - transform.position).normalized * breakForce;
            rb.AddExplosionForce(10, frac.transform.position,3);
        }

        Destroy(gameObject);
    }
}
