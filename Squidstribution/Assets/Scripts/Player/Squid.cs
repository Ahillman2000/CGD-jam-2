using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squid : MonoBehaviour, IDamageable
{
    [SerializeField] float health = 100;
    [SerializeField] int damage = 50;
    [SerializeField] float threat;

    private GameObject currentDistrict;
    private float timer = 0.0f;
    private const float cooldown = 0.5f;

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
            Destroy(gameObject);
        };
    }

    public void OnCollisionEnter(Collision col)
    {
        /// If the object collided with contains a IDamageable component, deal damage to it
        IDamageable hit = col.gameObject.GetComponent<IDamageable>();
        if (hit != null)
        {
            hit.ApplyDamage(damage);
            Debug.Log(col.gameObject + " took " + damage + " damage!");
        }
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

    public float GetCurrentDistrictDestruction()
    {
        District district = GetCurrentDistrict().GetComponent<District>();
        return district.GetDestruction();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SetThreat(GetThreat() + 1);
            Debug.Log(GetThreat());
        }
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
}
