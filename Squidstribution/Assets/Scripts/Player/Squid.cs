using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squid : MonoBehaviour, IDamageable
{
    [SerializeField] float health = 100;
    [SerializeField] int damage   = 50;
    [SerializeField] Camera followCam;

    // threat level 1 (scale 1x1x1), 2 (scale 2x2x2), 3 (scale 4x4x4), 4 (scale 8x8x8)
    [SerializeField] float threat = 1;
    [SerializeField] float karma = 0;

    private GameObject currentDistrict;

    private float timer          = 0.0f; 
    private const float cooldown = 1.0f;

    private bool attackAnimFinished = false;

    private void OnEnable()
    {
        EventManager.StartListening("SquidAttackAnimFinished", DealDamage);
    }

    private void OnDisable()
    {
        EventManager.StopListening("SquidAttackAnimFinished", DealDamage);
    }

    private void OnApplicationQuit()
    {
        Destroy(this);
        EventManager.StopListening("SquidAttackAnimFinished", DealDamage);
    }

    public void ApplyDamage(int damage) 
    {
        /// Restrict how much damage the player can recieve at once
        if (Time.time > timer)
        {
            timer = Time.time + cooldown;
            health -= damage;
        }

        if (health <= 0) { Destroy(gameObject); };
    } 

    private void DealDamage()
    {
        attackAnimFinished = true;
    }

    public void OnCollisionEnter(Collision col)
    {
        /// If the object collided with contains a IDamageable component, deal damage to it
        IDamageable hit = col.gameObject.GetComponent<IDamageable>();
        if (hit != null)
        {
            if (col.gameObject.GetComponent<MeshRenderer>() != null)
            {
                if(col.gameObject.GetComponent<Building>())
                {
                    col.gameObject.GetComponent<MeshRenderer>().material = col.gameObject.GetComponent<Building>().highlightMat;
                }
            }

            if (attackAnimFinished)
            {
                hit.ApplyDamage(damage);
                //Debug.Log(col.gameObject + " took " + damage + " damage!");
                attackAnimFinished = false;
            }
        }
    }

    private void OnCollisionExit(Collision col)
    {
        IDamageable hit = col.gameObject.GetComponent<IDamageable>();
        if (hit != null)
        {
            if (col.gameObject.GetComponent<MeshRenderer>() != null)
            {
                if (col.gameObject.GetComponent<Building>())
                {
                    col.gameObject.GetComponent<MeshRenderer>().materials = col.gameObject.GetComponent<Building>().defaultMat;
                }
            }
        }
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
                //meed to move camera with scale
                this.gameObject.transform.localScale = new Vector3(1, 1, 1);
                break;
            case 2:
                this.gameObject.transform.localScale = new Vector3(2, 2, 2);
                break;
            case 3:
                this.gameObject.transform.localScale = new Vector3(3, 3, 3);
                break;
            case 4:
                this.gameObject.transform.localScale = new Vector3(6, 6, 6);
                break;
            default:
                this.gameObject.transform.localScale = new Vector3(1, 1, 1);
                break;
        }
        followCam.fieldOfView += 10;
        followCam.transform.position = new Vector3(followCam.transform.position.x, followCam.transform.position.y + 9 + this.getScale(), followCam.transform.position.z);

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

    public float getScale()
    {
        return this.gameObject.transform.localScale.y;
    }
}
