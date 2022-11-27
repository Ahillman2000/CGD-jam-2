using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Squid : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject babySquidPrefab;
    [SerializeField] private float Maxhealth = 100.0f;
    private float health = 100.0f;
    [SerializeField] private int damage   = 50;
    [SerializeField] private Camera followCam;
    private Animator anim;
    private District currentDistrict;

    // threat level 1 (scale 1x1x1), 2 (scale 2x2x2), 3 (scale 3x3x3), 4 (scale 6x6x6)
    [SerializeField] float threat       = 1.0f;
    [SerializeField] float karma        = 0.0f;
    private float timer                 = 0.0f; 
    private const float cooldown        = 1.5f;
    private int pointsToNextSquid       = 100;
    private int pointsToNextThreatLevel = 600;
    private bool attacking     = false;

  
    private void Start()
    {
        SetHealth(Maxhealth);
        anim = gameObject.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventManager.StartListening("SquidAttackAnimFinished", EndAttack);
        EventManager.StartListening("SquidAttackAnimStarted", DealDamage);
    }

    private void OnDisable()
    {
        EventManager.StopListening("SquidAttackAnimFinished", EndAttack);
        EventManager.StopListening("SquidAttackAnimStarted", DealDamage);
    }

    private void OnApplicationQuit()
    {
        EventManager.StopListening("SquidAttackAnimFinished", EndAttack);
        EventManager.StopListening("SquidAttackAnimStarted", DealDamage);
        Destroy(this);
    }

    void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            IncreaseThreat();
        }
#endif

        if(GetKarma() >= pointsToNextThreatLevel)
        {
            IncreaseThreat();
            pointsToNextThreatLevel += 800;
            EventParam eventParam = new EventParam(); eventParam.float_ = GetThreat();
            EventManager.TriggerEvent("ThreatLevelChange", eventParam);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(GetKarma() >= pointsToNextSquid)
            {
                GameObject babysquid = Instantiate(babySquidPrefab, transform.position, Quaternion.identity);
                babysquid.GetComponent<BabySquid>().pathFindTarget = transform;
                babysquid.transform.parent = this.transform;
                SetKarma(GetKarma() - (pointsToNextSquid / 2));
                pointsToNextSquid += 100;
                EventParam eventParam = new EventParam(); eventParam.soundstr_ = "SquidSpawn";
                EventManager.TriggerEvent("BabySquidSpawned", eventParam);
            }
        }

        if(GetHealth() > GetMaxHealth())
        {
            SetHealth(Maxhealth);
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

    private void DealDamage(EventParam eventParam)
    {
        attacking = true;
    }

    private void EndAttack(EventParam eventParam)
    {
        attacking = false;
    }

    public void OnCollisionEnter(Collision col)
    {
        if ((col.gameObject.GetComponent<Destructable>() != null && !col.gameObject.GetComponent<Destructable>().isActiveAndEnabled) ||
                   (col.gameObject.GetComponent<Building>() != null && !col.gameObject.GetComponent<Building>().isActiveAndEnabled) ||
                   (col.gameObject.GetComponent<Soldier>() != null && !col.gameObject.GetComponent<Soldier>().isActiveAndEnabled) ||
                   (col.gameObject.GetComponent<lever>() != null && !col.gameObject.GetComponent<lever>().isActiveAndEnabled))
        {
            return;
        }

        // temporary solution
        if (col.gameObject.GetComponent<MeshRenderer>() != null)
        {
            if (col.gameObject.GetComponent<Building>() != null)
            {
                col.gameObject.GetComponent<MeshRenderer>().material = col.gameObject.GetComponent<Building>().highlightMat;
                if (getScale() >= col.gameObject.GetComponent<Building>().size_factor && col.gameObject.GetComponent<Building>().GetHealth() > 0)
                {
                    IDamageable hit = col.gameObject.GetComponent<IDamageable>();
                    hit.ApplyDamage(col.gameObject.GetComponent<Building>().GetHealth());
                }
            }

            if (col.gameObject.GetComponent<Destructable>() != null)
            {
                col.gameObject.GetComponent<MeshRenderer>().material = col.gameObject.GetComponent<Destructable>().highlightMat;
                if (getScale() >= col.gameObject.GetComponent<Destructable>().GetSize() && col.gameObject.GetComponent<Destructable>().GetHealth() > 0)
                {
                    IDamageable hit = col.gameObject.GetComponent<IDamageable>();
                    hit.ApplyDamage(col.gameObject.GetComponent<Destructable>().GetHealth());
                }
            }

            if (col.gameObject.GetComponent<lever>() != null)
            {
                col.gameObject.GetComponent<MeshRenderer>().material = col.gameObject.GetComponent<lever>().highlightMat;
            }
        }

        if (attacking)
        {
            /// If the object collided with contains a IDamageable component, deal damage to it
            IDamageable hit = col.gameObject.GetComponent<IDamageable>();
            if (hit != null)
            {

                hit.ApplyDamage(damage);
                //Debug.Log(col.gameObject + " took " + damage + " damage!");

                // temporary lazy solution - instead send damageable event
                if (col.gameObject.GetComponent<Destructable>() != null)
                {
                    EventParam eventParam = new EventParam(); eventParam.soundstr_ = "HitBuilding";
                    EventManager.TriggerEvent("CarDamaged", eventParam);
                }
                if (col.gameObject.GetComponent<Building>() != null)
                {
                    EventParam eventParam = new EventParam(); eventParam.soundstr_ = "HitBuilding";
                    EventManager.TriggerEvent("BuildingDamaged", eventParam);
                }
                if (col.gameObject.GetComponent<Soldier>() != null)
                {
                    EventParam eventParam = new EventParam(); eventParam.soundstr_ = "EnemyHit";
                    EventManager.TriggerEvent("SoldierHit", eventParam);
                }

                if (col.gameObject.GetComponent<lever>() != null)
                {
                    GetComponent<NavMeshAgent>().Warp(col.gameObject.GetComponent<lever>().GetWarp());
                }
                attacking = false;
            }
        }
        if (col.gameObject.CompareTag("Beach"))
        {
            EventParam param = new EventParam();
            EventManager.TriggerEvent("WasOnBeach", param);
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

        if (col.gameObject.GetComponent<Destructable>() != null)
        {
            col.gameObject.GetComponent<MeshRenderer>().materials = col.gameObject.GetComponent<Destructable>().defaultMat;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("District"))
        {
            Squid squid = this.GetComponent<Squid>();

            squid.SetCurrentDistrict(other.gameObject.GetComponent<District>());
            squid.GetCurrentDistrict().ActivateSpawners();
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

    public float GetMaxHealth()
    {
        return Maxhealth;
    }

    public void IncreaseHealth(float addition)
    {
        Maxhealth += addition;
        health += addition;
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

    public float GetKarmaTillNextThreat()
    {
        return pointsToNextThreatLevel;
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
                IncreaseHealth(120);
                break;
            case 3:
                this.gameObject.transform.localScale = new Vector3(3, 3, 3);
                IncreaseHealth(120);
                break;
            case 4:
                this.gameObject.transform.localScale = new Vector3(6, 6, 6);
                IncreaseHealth(130);
                break;
            default:
                this.gameObject.transform.localScale = new Vector3(1, 1, 1);
                break;
        }
        
        followCam.fieldOfView += 8.5f;
        followCam.transform.position = new Vector3(followCam.transform.position.x, followCam.transform.position.y + 9.75f + getScale(), followCam.transform.position.z);

        //this.gameObject.transform.localScale = new Vector3(_scale, _scale, _scale);
    }

    public void SetThreat(float _threat)
    {
        threat = _threat;
        setScale(threat);
    }
    public void IncreaseThreat()
    {
        if(threat < 4)
        {
            threat++;
            setScale(threat);
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

    public float getScale()
    {
        return this.gameObject.transform.localScale.y;
    }

    public int GetPointsToNextSquid()
    {
        return pointsToNextSquid;
    }

    public void InCutscene()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;
    }
    public void OutCutscene()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        gameObject.GetComponent<SphereCollider>().enabled = true;
    }

    IEnumerator PlayDeath()
    {
        yield return new WaitForSecondsRealtime(1.25f);
        Destroy(gameObject);
        SceneManager.LoadScene("Badend");
    }

    public IEnumerator WaitAfterTrigger(float time, Camera newscam, Camera followcam)
    {
        yield return new WaitForSeconds(time);
        newscam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        followcam.GetComponent<CinemachineVirtualCamera>().Priority = 1;
    }
}
