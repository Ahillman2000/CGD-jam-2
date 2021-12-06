using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    [SerializeField] District district;

    /*[SerializeField]*/ private int health;
    [SerializeField] private float startHealth = 100.0f;
    [SerializeField] private float KarmaScore = 25.0f;

    //[HideInInspector] public Material defaultMat;
    [HideInInspector] public Material[] defaultMat;
    public Material highlightMat;
    private Slider slider;
    public int size_factor;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        slider.gameObject.SetActive(false);
        
        if (GetComponent<MeshRenderer>() != null)
        {
            defaultMat = GetComponent<MeshRenderer>().materials;
        }

        if(district != null)
        {
            district.SetBuildingCount(district.GetBuildingCount());
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

    private void UpdateSlider()
    {
        float currentHealthPCT = (float)health / (float)startHealth;
        slider.value = currentHealthPCT;
        if(slider.value < 1)
        {
            slider.gameObject.SetActive(true);
        }

        // change this. BAD. Just for testing.
        slider.transform.rotation = new Quaternion(slider.transform.rotation.x, Camera.main.transform.rotation.y, slider.transform.rotation.z, slider.transform.rotation.w);
    }

    public float GetStartHealth()
    {
        return startHealth;
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int health_)
    {
        health = health_;
    }

    public District GetDistrict()
    {
        return district;
    }

    public float GetKarmaScore()
    {
        return KarmaScore;
    }
}
