using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squid : MonoBehaviour
{
    [SerializeField] float health = 100;

    // threat level 1 (scale 1x1x1), 2 (scale 2x2x2), 3 (scale 4x4x4), 4 (scale 8x8x8)
    [SerializeField] float threat;
    [SerializeField] float karma = 0;

    private GameObject currentDistrict;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("District"))
        {
            Squid squid = this.GetComponent<Squid>();

            squid.SetCurrentDistrict(other.gameObject);
            Debug.Log("Player has entered " + GetCurrentDistrict());
            Debug.Log("Destruction for this district is " + GetCurrentDistrictDestruction() + "%");
        }
    }

    public void SetHealth(float _health)
    {
        health = _health;
    }
    public float GetHealth()
    {
        return health;
    }

    public void SetCurrentDistrict(GameObject _district)
    {
        currentDistrict = _district;
    }
    public GameObject GetCurrentDistrict()
    {
        return currentDistrict;
    }

    public void SetCurrentDistrictDestruction(float _destruction)
    {
        District currentDistrict = GetCurrentDistrict().GetComponent<District>();
        currentDistrict.SetDestruction(_destruction);
    }
    public float GetCurrentDistrictDestruction()
    {
        District currentDistrict = GetCurrentDistrict().GetComponent<District>();
        return currentDistrict.GetDestruction();
    }

    public float GetDestructionpointsPerBuildingInCurrentDistrict()
    {
        District currentDistrict = GetCurrentDistrict().GetComponent<District>();
        return currentDistrict.GetDestruction();
    }

    void setScale(float _scale)
    {
        this.gameObject.transform.localScale = new Vector3(_scale, _scale, _scale);
    }

    public void SetThreat(float _threat)
    {
        threat = _threat;

        if (threat <= 33)
        {
            setScale(1);
        }
        else if (threat > 33 && threat <= 66)
        {
            setScale(2);
        }
        else if (threat > 66 && threat <= 99)
        {
            setScale(4);
        }
        else if (threat > 99)
        {
            setScale(8);
        }
    }
    public float GetThreat()
    {
        return threat;
    }

    public void SetKarma(float _karma)
    {
        karma = _karma;
    }
    public float GetKarma()
    {
        return karma;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //SetThreat(GetThreat() + 1);
            //Debug.Log(GetThreat());

            // When building is destroyed, increase by destructionPointsPerBuilding
            SetCurrentDistrictDestruction(GetCurrentDistrictDestruction() + 1);
        }
    }
}
