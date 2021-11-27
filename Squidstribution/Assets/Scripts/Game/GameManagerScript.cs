using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] float threat = 0;
    [SerializeField] float destruction = 0;
    [SerializeField] float karma = 0;

    private int buildingCount = 0;
    private float destructionpointsPerBuilding;

    void Start()
    {
        //Debug.Log(buildingCount);
        destructionpointsPerBuilding = 100 / buildingCount;

        Debug.Log("each building is worth " + destructionpointsPerBuilding + " points");
    }

    void Update()
    {

    }

    public void SetThreat(float _threat)
    {
        threat = _threat;
    }
    public float GetThreat()
    {
        return threat;
    }

    public void SetDestruction(float _destruction)
    {
        destruction = _destruction;
    }
    public float GetDestruction()
    {
        return destruction;
    }

    public void SetKarma(float _karma)
    {
        karma = _karma;
    }
    public float GetKarma()
    {
        return karma;
    }

    public void SetBuildingCount(int _buildingCount)
    {
        buildingCount = _buildingCount;
    }
    public int GetBuildingCount()
    {
        return buildingCount;
    }

    public float GetDestructionpointsPerBuilding()
    {
        return destructionpointsPerBuilding;
    }
}
