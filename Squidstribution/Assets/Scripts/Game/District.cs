using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class District : MonoBehaviour
{
    [SerializeField] float destruction = 0;
    [SerializeField] List<GameObject> DistrictSpawners;

    [HideInInspector] public int maxBuildingCount = 0;
    private int buildingCount = 0;
    private float destructionPointsPerBuilding;
    private float timer = 0.25f;
    private bool stupidCheck = false;

    void Start()
    {
        foreach (GameObject spawner in DistrictSpawners)
        {
            spawner.SetActive(false);
        }
        SetDestructionPointsPerBuilding();
        Debug.Log(this.gameObject.name + " has " + buildingCount + " buildings, each worth " + destructionPointsPerBuilding + " points");
    }

    private void Update()
    {
        if (Time.time > timer && !stupidCheck) // sorry, horrible way to do it but I'm tired and need to sleep. Will fix up nicer soon. Charlie
        {
            maxBuildingCount = buildingCount;
            stupidCheck = true;
        }
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

    public void ActivateSpawners()
    {
        foreach(GameObject spawner in DistrictSpawners)
        {
            spawner.SetActive(true);
        }
    }

    public void DectivateSpawners()
    {
        foreach (GameObject spawner in DistrictSpawners)
        {
            spawner.SetActive(false);
        }
    }
}
