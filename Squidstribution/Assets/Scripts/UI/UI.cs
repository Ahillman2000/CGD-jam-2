using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Text destructionText, kramaText;
    [SerializeField] private Slider healthSlider, threatSlider;

    private GameObject player;
    [SerializeField] private int playerHealth, fullHealth, threat, krama, destruction;
    private bool paused;

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = fullHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = playerHealth;
        threatSlider.value = threat;
        kramaText.text = "Krama: " + krama;
        destructionText.text = "Destruction: " + destruction + "%";
    }

    public void Menu()
    {
        if (paused)
        {
            Time.timeScale = 1;
            paused = false;
        }
        else
        {
            Time.timeScale = 0;
            paused = true;
        }
    }
}
