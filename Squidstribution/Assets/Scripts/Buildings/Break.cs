using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor.AI;

public class Break : MonoBehaviour, IDamageable
{
    [SerializeField]
    private GameObject fractured;
    [SerializeField]
    private float breakForce;
    private Building buildingStats;
    CalculateKarma calc;

    private static int buildingsDestroyed;

    private void Start()
    {
        buildingStats = gameObject.GetComponent<Building>();
        calc = GameObject.Find("KarmaKalculator").GetComponent<CalculateKarma>();
        if (buildingStats.GetDistrict() != null)
        {
            buildingStats.GetDistrict().SetBuildingCount(buildingStats.GetDistrict().GetBuildingCount() + 1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player") && collision.gameObject.GetComponent<Squid>().getScale() >= buildingStats.size_factor)
        {
            ApplyDamage(buildingStats.GetHealth());
        }
    }

    private void Update()
    {
        /*if (buildingStats.GetDistrict() != null)
        {
            buildingStats.GetDistrict().SetBuildingCount(buildingStats.GetDistrict().GetBuildingCount());
        }*/
    }

    public void ApplyDamage(int damage)
    {
        buildingStats.SetHealth(buildingStats.GetHealth() - damage);

        ParticleSystem particles = GetComponentInChildren<ParticleSystem>();
        if (particles != null)
        {
            particles.Play(true);
        }

        if (buildingStats.GetHealth() <= 0)
        {
            BreakThing();
            buildingStats.GetDistrict().SetBuildingCount(buildingStats.GetDistrict().GetBuildingCount() - 1);
        }
    }

    private void BreakThing()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        GameObject frac = Instantiate(fractured, gameObject.transform.position, transform.rotation);

        foreach(Rigidbody rb in frac.GetComponentsInChildren<Rigidbody>())
        {
            rb.AddExplosionForce(breakForce, frac.transform.position,3);
        }
        buildingStats.GetDistrict().SetDestruction(buildingStats.GetDistrict().GetDestruction() + buildingStats.GetDistrict().GetDestructionPointsPerBuilding());
        calc.setBuildingValue(buildingStats.GetKarmaScore());
        Destroy(gameObject);
        buildingsDestroyed++;
        EventManager.TriggerEvent("BuildingDestroyed", new EventParam());

        // but you're still coupling aren't you if the breaking needs to know about the event manager?
        // but I mean it still works I guess so that's fine. It's just the idea of this long list of
        // achievment stuff in the break script doesn't seem quite right to me

        /// Sure, but it is not tightly coupled. There is no need for having to cache a reference to the
        /// event manager as it's methods are static. If this were done in an event manager class the 
        /// class would soon spiral out of control as more and more references to different components 
        /// are needed to be cached.
        /// Yeah having it in the break script is not ideal I agree. Instead it should simply just 
        /// broadcast whenever a building has been destroyed. Then the achievement system can handle 
        /// the logic for when an achievement is triggered. A stats class could be used to collate kills,
        /// buildings destroyed, karma, etc and then the achievement system would just need to check 
        /// against these conditions. - Charlie
        /// Update: Better solution implemented now, tho still could be better
    }
}
