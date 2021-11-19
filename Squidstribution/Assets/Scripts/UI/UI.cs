using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Text destructionText, karmaText;
    [SerializeField] private Slider healthSlider, threatSlider;
    [SerializeField] private GameObject pausePanel, menuButton;

    private GameObject player;
    [SerializeField] private int playerHealth, fullHealth, threat, karma, destruction;
    private bool paused;

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = fullHealth;
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = playerHealth;
        threatSlider.value = threat;
        karmaText.text = "Karma: " + karma;
        destructionText.text = "Destruction: " + destruction + "%";
    }

    public void Menu()
    {
        Time.timeScale = 0;
        paused = true;
        pausePanel.SetActive(true);
        menuButton.SetActive(false);
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        paused = false;
        pausePanel.SetActive(false);
        menuButton.SetActive(true);
    }
}
