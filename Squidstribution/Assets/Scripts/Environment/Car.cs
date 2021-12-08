using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour, IDamageable
{
    private int health;
    [SerializeField] private float startHealth = 100.0f;
    [HideInInspector] public Material[] defaultMat;
    public Material highlightMat;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        slider.gameObject.SetActive(false);

        if (GetComponent<MeshRenderer>() != null)
        {
            defaultMat = GetComponent<MeshRenderer>().materials;
        }
    }

    private void Start()
    {
        health = (int)startHealth;
    }

    private void Update()
    {
        UpdateSlider();
    }

    public void ApplyDamage(int damage)
    {
        health -= damage;

        ParticleSystem particles = GetComponentInChildren<ParticleSystem>();
        if (particles != null)
        {
            particles.Play(true);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
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
}


