using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squid : MonoBehaviour
{
    [SerializeField] float health = 100;

    // threat level 1 (scale 1x1x1), 2 (scale 2x2x2), 3 (scale 4x4x4), 4 (scale 8x8x8)
    [SerializeField] float threat = 1;
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
            //Debug.Log("Player has entered " + GetCurrentDistrict());
            //Debug.Log("Destruction for this district is " + GetCurrentDistrictDestruction() + "%");
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
        switch (_scale)
        {
            case 1:
                this.gameObject.transform.localScale = new Vector3(1, 1, 1);
                break;
            case 2:
                this.gameObject.transform.localScale = new Vector3(2, 2, 2);
                break;
            case 3:
                this.gameObject.transform.localScale = new Vector3(4, 4, 4);
                break;
            case 4:
                this.gameObject.transform.localScale = new Vector3(8, 8, 8);
                break;
            default:
                this.gameObject.transform.localScale = new Vector3(1, 1, 1);
                break;
        }


        //this.gameObject.transform.localScale = new Vector3(_scale, _scale, _scale);
    }

    public void SetThreat(float _threat)
    {
        threat = _threat;
        setScale(threat);
    }
    public void IncreaseThreat()
    {
        threat++;
        setScale(threat);
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
            IncreaseThreat();
        }
    }
}
