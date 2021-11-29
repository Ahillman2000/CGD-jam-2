using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class District : MonoBehaviour
{
    [SerializeField] float destruction = 0;

    private int buildingCount = 0;
    private float destructionPointsPerBuilding;

    void Start()
    {
        SetDestructionPointsPerBuilding();
        Debug.Log(this.gameObject.name + " has " + buildingCount + " buildings, each worth " + destructionPointsPerBuilding + " points");
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Squid squid = player.GetComponent<Squid>();

            squid.SetCurrentDistrict(this.gameObject);
            Debug.Log("Player has entered " + squid.GetCurrentDistrict());
        }
    }*/

    public void SetBuildingCount(int _buildingCount)
    {
        buildingCount = _buildingCount;
    }
    public int GetBuildingCount()
    {
        return buildingCount;
    }

    public void SetDestructionPointsPerBuilding()
    {
        if (buildingCount > 0)
        {
            destructionPointsPerBuilding = 100 / buildingCount;
        }
    }
    public float GetDestructionPointsPerBuilding()
    {
        return destructionPointsPerBuilding;
    }

    public void SetDestruction(float _destruction)
    {
        destruction = _destruction;
    }
    public float GetDestruction()
    {
        return destruction;
    }

    void Update()
    {

    }
}
