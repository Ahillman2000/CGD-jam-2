using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Text destructionText, threatText, districtText, targetText, popupText;
    [SerializeField] private Slider healthSlider, targetSlider;
    [SerializeField] private GameObject pausePanel, baseObject, newsOverlay, menuButton, target, popup, ticker;

    [SerializeField] private GameObject player, targetObject;
    private string targetName;
    public bool paused, targetSet, onMenuButton, baseOn, newsOn;
    private bool popupOn, genNews;
    //using the break script rather than the building script, believe everything is in there though that building had
    //private Building building;

    private Squid squid;
    private Ticker tickerScript;

    private string currentDistrict, newDistrict;

    // Start is called before the first frame update
    void Start()
    {
        squid = player.GetComponent<Squid>();
        healthSlider.maxValue = squid.GetHealth();
        tickerScript = ticker.GetComponent<Ticker>();
        pausePanel.SetActive(false);
        target.SetActive(false);
        popup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = squid.GetHealth();
        if (squid.GetCurrentDistrict() == null)
        {
            districtText.text = "District: none";
            destructionText.text = "Destruction Karma: 0%";
            currentDistrict = "None";
        }
        else
        {
            newDistrict = squid.GetCurrentDistrict().name;
            districtText.text = "District: " + squid.GetCurrentDistrict().name;
            destructionText.text = "Destruction Karma: " + squid.GetKarma();
        }
        if (currentDistrict != newDistrict)
        {
            currentDistrict = newDistrict;
            PopUp("Entering " + currentDistrict);
        }
        threatText.text = "Threat Level: " + squid.GetThreat();
        if (targetObject != null)
        {
            if (!targetSet)
            {
                string[] sp = targetObject.name.Split('_');
                targetName = sp[0];
                target.SetActive(true);
                targetText.text = targetName;
                /*if (targetObject.GetComponent<Building>() != null)
                {
                    building = targetObject.GetComponent<Building>();
                    targetSlider.maxValue = building.GetStartHealth();

                }*/
                targetSet = true;
            }
            /*if (targetObject.GetComponent<Building>() != null)
            {
                targetSlider.value = building.GetHealth();
                if (targetSlider.value <= 0)
                {
                    targetObject = null;
                }
            }*/
        }
        else if (targetSet)
        {
            StartCoroutine(DelayTargetDis());
        }
        if (healthSlider.value <= healthSlider.maxValue / 10)
        {
            PopUp("Squid low health");
        }
        if (popupOn)
        {
            StartCoroutine(HandlePopup());
        }
        if (newsOn)
        {
            newsOverlay.SetActive(true);
            if (!genNews)
            {
                genNews = true;
                tickerScript.GenOneNews();
            }
        }
        else
        {
            newsOverlay.SetActive(false);
            genNews = false;
        }
        if (baseOn)
        {
            baseObject.SetActive(true);
        }
        else
        {
            baseObject.SetActive(false);
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

    public void PopUp(string message)
    {
        popupText.text = message;
        popupOn = true;
    }

    IEnumerator HandlePopup()
    {
        popupOn = false;
        popup.SetActive(true);
        yield return new WaitForSeconds(2);
        popup.SetActive(false);
    }

}
