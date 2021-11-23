using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour
{
    [SerializeField] float explosionForce  = 100;
    [SerializeField] float explosionRadius = 100;
    [SerializeField] float upwardsModifier = 1;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //create a ray cast and set it to the mouses cursor position in game
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, float.PositiveInfinity))
            {
                //draw invisible ray cast/vector
                Debug.DrawLine(ray.origin, hit.point);

                GameObject hitObject = hit.transform.gameObject;

                Debug.Log(hitObject.tag);

                if(hitObject.CompareTag("IntactBuilding"))
                {
                    Building buildingScript = hitObject.GetComponent<Building>();
                    buildingScript.TakeDamage(100);
                }

                if(hitObject.CompareTag("DestroyedBuilding"))
                {
                    Rigidbody hitObjectRB = hitObject.GetComponent<Rigidbody>();
                    hitObjectRB.AddExplosionForce(explosionForce, hit.point, explosionRadius, upwardsModifier, ForceMode.Impulse);
                }

            }
        }
    }

}
