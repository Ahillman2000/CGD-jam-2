using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Text destructionText, karmaText;
    [SerializeField] private Slider healthSlider, threatSlider;
    [SerializeField] private GameObject pausePanel, menuButton, gameManager;

    private GameObject player;
    [SerializeField] private int playerHealth, fullHealth;
    private bool paused;
    private GameManagerScript script;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healthSlider.maxValue = fullHealth;
        pausePanel.SetActive(false);
        script = gameManager.GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = playerHealth;
        threatSlider.value = script.GetThreat();
        karmaText.text = "Karma: " + script.GetKarma().ToString();
        destructionText.text = "Destruction: " + script.GetDestruction().ToString() + "%";
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
