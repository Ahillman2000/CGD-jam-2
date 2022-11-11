using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class lever : MonoBehaviour, IDamageable
{

    int health = 50;
    int startHealth = 50;

    private Animator anim;

    [SerializeField]
    private GameObject fractured1;
    [SerializeField]
    private GameObject fractured2;
    [SerializeField]
    private Vector3 WarpLocation;

    private GameObject ActiveBuilding;

    [HideInInspector] public Material[] defaultMat;
    public Material highlightMat;
    private Slider slider;


    private void Awake()
    {
        ActiveBuilding = fractured1;

        if(GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }
        if (GetComponentInChildren<Slider>() != null)
        {
            slider = GetComponentInChildren<Slider>();
            slider.gameObject.SetActive(false);
        }


        if (GetComponent<MeshRenderer>() != null)
        {
            defaultMat = GetComponent<MeshRenderer>().materials;
        }
    }

    private void Update()
    {
        if (slider != null)
            UpdateSlider();

    }

    private void UpdateSlider()
    {
        float currentHealthPCT = (float)health / (float)startHealth;
        slider.value = currentHealthPCT;
        if (slider.value < 1)
        {
            slider.gameObject.SetActive(true);
        }

        // change this. BAD. Just for testing.
        slider.transform.rotation = new Quaternion(slider.transform.rotation.x, Camera.main.transform.rotation.y, slider.transform.rotation.z, slider.transform.rotation.w);
    }

    private void SetHealth(int health_)
    {
        health = health_;
    }

    private int GetHealth()
    {
        return health;
    }

    public void ApplyDamage(int damage)
    {
        SetHealth(GetHealth() - damage);

        ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
        if (particles.Length != 0)
        {
            foreach (ParticleSystem particle in particles)
            {
                particle.Play();
            }
        }

        if (GetHealth() <= 0)
        {
            BreakThing();
        }
    }

    private void BreakThing()
    {
        if (fractured1 == null || fractured2 == null)
        {
            gameObject.SetActive(false);
            return;
        }
        if(fractured1.activeInHierarchy)
        {
            fractured2.SetActive(true);
            fractured1.SetActive(false);
            ActiveBuilding = fractured2;
        }
        else
        {
            fractured1.SetActive(true);
            fractured2.SetActive(false);
            ActiveBuilding = fractured1;
        }
        SetHealth(startHealth);
    }

    public Vector3 GetWarp()
    {
        return WarpLocation;
    }

    public GameObject GetActiveBuilding()
    {
        return ActiveBuilding;
    }
}
