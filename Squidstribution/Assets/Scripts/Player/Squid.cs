using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Squid : MonoBehaviour, IDamageable
{
    [SerializeField] float health = 100;
    [SerializeField] int damage   = 50;
    [SerializeField] Camera followCam;
    private Animator anim;

    // threat level 1 (scale 1x1x1), 2 (scale 2x2x2), 3 (scale 3x3x3), 4 (scale 6x6x6)
    [SerializeField] float threat = 1;
    [SerializeField] float karma = 0;

    private District currentDistrict;

    private float timer          = 0.0f; 
    private const float cooldown = 1.5f;

    private bool attackAnimFinished = false;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IncreaseThreat();
        }
    }

    public void ApplyDamage(int damage) 
    {
        /// Restrict how much damage the player can recieve at once
        if (Time.time > timer)
        {
            timer = Time.time + cooldown;
            health -= damage;
        }

        if (health <= 0) 
        {
            anim.SetTrigger("IsDead");
            StartCoroutine(PlayDeath());
        };
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
                if(col.gameObject.GetComponent<Building>() != null)
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
                if (col.gameObject.GetComponent<Building>() != null)
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

            squid.SetCurrentDistrict(other.gameObject.GetComponent<District>());
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

    /*public void SetCurrentDistrict(GameObject _district) 
    {
        currentDistrict = _district;
    }*/

    /*public GameObject GetCurrentDistrict()
    {
        return currentDistrict;
    }*/

    public void SetCurrentDistrict(District district) // charlie
    {
        currentDistrict = district;
    }

    public District GetCurrentDistrict() // charlie
    {
        return currentDistrict;
    }

    public void SetCurrentDistrictDestruction(float _destruction)
    {
        District currentDistrict = GetCurrentDistrict();
        currentDistrict.SetDestruction(_destruction);
    }
    public float GetCurrentDistrictDestruction()
    {
        District currentDistrict = GetCurrentDistrict();
        return currentDistrict.GetDestruction();
    }

    public float GetDestructionpointsPerBuildingInCurrentDistrict()
    {
        District currentDistrict = GetCurrentDistrict();
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
        followCam.fieldOfView += 8;
        followCam.transform.position = new Vector3(followCam.transform.position.x, followCam.transform.position.y + 9.5f + this.getScale(), followCam.transform.position.z);

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

    public float getScale()
    {
        return this.gameObject.transform.localScale.y;
    }

    IEnumerator PlayDeath()
    {
        yield return new WaitForSecondsRealtime(3.75f);
        Destroy(gameObject);
        SceneManager.LoadScene("Badend");
    }
}
