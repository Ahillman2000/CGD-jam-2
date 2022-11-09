using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshObstacle))]
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
    private UnifiedHealthBar unibar;

    private void Awake()
    {
        if (GetComponentInChildren<Slider>() != null)
        {
            slider = GetComponentInChildren<Slider>();
            slider.gameObject.SetActive(false);
        }
        else
        {
            unibar = transform.parent.GetComponentInChildren<UnifiedHealthBar>(true);
        }
        
        
        if (GetComponent<MeshRenderer>() != null)
        {
            defaultMat = GetComponent<MeshRenderer>().materials;
        }

        if(district != null)
        {
            district.SetBuildingCount(district.GetBuildingCount() + 1);
        }
    }

    private void Start()
    {
        health = (int)startHealth;
    }

    private void Update()
    {
        if(slider != null)
            UpdateSlider();

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.G))
        {
            Destroy(gameObject);
        }
#endif
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
        if (slider == null)
        {
            unibar.gameObject.SetActive(true);
            if (health < 0)
            {
                health = 0;
            }
            unibar.SetUnifiedHealth();
        }
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
