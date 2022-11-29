using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Destructable : MonoBehaviour, IDamageable
{
    private int health;
    [SerializeField] private float startHealth = 100.0f;
    [HideInInspector] public Material[] defaultMat;
    public Material highlightMat;
    private Slider slider;
    [SerializeField] int SizeFactor = 1;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();

        if (slider != null)
        {
            slider.gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }

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
            if (GetComponent<Rigidbody>() != null)
            {
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * (40 * GameObject.FindGameObjectWithTag("Player").GetComponent<Squid>().GetThreat()), ForceMode.Impulse);
            }
            DisableGO();
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

    public int GetSize()
    {
        return SizeFactor;
    }

    public int GetHealth()
    {
        return health;
    }

    void DisableGO()
    {
        if (gameObject.tag == "IceCream")
        {
            EventParam eventParam = new EventParam();
            eventParam.soundstr_ = "EatSFX";
            EventManager.TriggerEvent("IceCreamEaten", eventParam);
        }

        if(GetComponent<Despawn>() != null)
        {
            GetComponent<Despawn>().enabled = true;
            for (int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).GetComponent<Despawn>() != null)
                {
                    transform.GetChild(i).GetComponent<Despawn>().enabled = true;

                    if (transform.GetChild(i).childCount > 0)
                    {
                        for (int j = 0; j < transform.GetChild(i).childCount; j++)
                        {
                            if (transform.GetChild(i).GetChild(j).GetComponent<Despawn>() != null)
                            {
                                transform.GetChild(i).GetChild(j).GetComponent<Despawn>().enabled = true;
                            }
                        }
                    }
                }
            }            
            return;
        }
        

        gameObject.SetActive(false);
    }
}


