using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Text destructionText, karmaText, targetText;
    [SerializeField] private Slider healthSlider, threatSlider, targetSlider;
    [SerializeField] private GameObject pausePanel, menuButton, gameManager, target;

    private GameObject player, targetObject;
    [SerializeField] private float playerHealth, fullHealth;
    private string targetName;
    private bool paused, targetSet;
    private GameManagerScript script;
    private Building building;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healthSlider.maxValue = fullHealth;
        pausePanel.SetActive(false);
        target.SetActive(false);
        script = gameManager.GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = playerHealth;
        threatSlider.value = script.GetThreat();
        karmaText.text = "Karma: " + script.GetKarma().ToString();
        destructionText.text = "Destruction: " + script.GetDestruction().ToString() + "%";
        if (targetObject != null)
        {
            if (!targetSet)
            {
                building = targetObject.GetComponent<Building>();
                targetName = targetObject.name;
                target.SetActive(true);
                targetText.text = targetName;
                targetSet = true;
            }
            targetSlider.value = building.GetHealth();
        }
        else
        {
            target.SetActive(false);
            targetSet = false;
        }
    }

    public void SettargetObject(GameObject _targetObject)
    {
        targetObject = _targetObject;
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
