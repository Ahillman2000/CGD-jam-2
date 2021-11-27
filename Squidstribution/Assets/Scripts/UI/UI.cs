using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Text destructionText, karmaText, targetText;
    [SerializeField] private Slider healthSlider, threatSlider, targetSlider;
    [SerializeField] private GameObject pausePanel, menuButton, gameManager, target;

    [SerializeField] private GameObject player, targetObject;
    private string targetName;
    public bool paused, targetSet, onMenuButton;
    private GameManagerScript gameManagerScript;
    private Building building;

    private Squid squid;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
        squid = player.GetComponent<Squid>();

        healthSlider.maxValue = squid.getHealth();
        pausePanel.SetActive(false);
        target.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = squid.getHealth();
        threatSlider.value = gameManagerScript.GetThreat();
        karmaText.text = "Karma: " + gameManagerScript.GetKarma().ToString();

        if (squid.GetCurrentDistrict() == null)
        {
            destructionText.text = "Destruction: 0%";
        }
        else
        {
            destructionText.text = "Destruction: " + squid.GetCurrentDistrictDestruction() + "%";
        }
        
        if (targetObject != null)
        {
            if (targetObject.GetComponent<Building>() != null)
            {
                if (!targetSet)
                {
                    building = targetObject.GetComponent<Building>();
                    string[] sp = targetObject.name.Split('_');
                    targetName = sp[0];
                    target.SetActive(true);
                    targetText.text = targetName;
                    targetSlider.maxValue = building.GetStartHealth();
                    targetSet = true;
                }
                targetSlider.value = building.GetHealth();
                if (targetSlider.value <= 0)
                {
                    targetObject = null;
                }
            }
        }
        else if (targetSet)
        {
            StartCoroutine(DelayTargetDis());
        }
    }

    public void SettargetObject(GameObject _targetObject)
    {
        targetObject = _targetObject;
    }

    IEnumerator DelayTargetDis()
    {
        targetSet = false;
        yield return new WaitForSeconds(1);
        target.SetActive(false);
    }

    public void Menu()
    {
        paused = true;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        menuButton.SetActive(false);
    }

    public void Unpause()
    {
        StartCoroutine(DelayUnPause());
    }

    IEnumerator DelayUnPause()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        menuButton.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        paused = false;
    }

    public void EnterMenuButton()
    {
        onMenuButton = true;
    }

    public void LeaveMenuButton()
    {
        onMenuButton = false;
    }
}
