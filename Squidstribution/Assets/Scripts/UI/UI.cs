using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Image achievementImage;
    [SerializeField] private Text karmaText, threatText, districtText, targetText, popupText, ptsForNextSquid;
    [SerializeField] private Slider healthSlider, targetSlider, ThreatSlider;
    [SerializeField] private GameObject pausePanel, baseObject, newsOverlay, menuButton,  popup, ticker, BossUI;
    [SerializeField] private GameObject player, targetObject;
    [SerializeField] private GameObject pBar;
    [SerializeField] TextMeshProUGUI healthpoints;
    //private string targetName;
    [HideInInspector] public bool paused, targetSet, onMenuButton, baseOn;
    private bool popupOn, genNews, newsOn;
    //private Building building;

    private Squid squid;
    private Ticker tickerScript;

    private string currentDistrict, newDistrict;

    private bool achievementUnlocked = false;

    private void OnEnable()
    {
        EventManager.StartListening("AchievementEarned", PopUp);
    }

    private void OnDisable()
    {
        EventManager.StopListening("AchievementEarned", PopUp);
    }

    private void OnApplicationQuit()
    {
        Destroy(this);
        EventManager.StopListening("AchievementEarned", PopUp);
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.2f);
        pBar.GetComponent<ProgressBar>().SetCurrent(squid.GetCurrentDistrict().maxBuildingCount, squid.GetCurrentDistrict().name);
        yield return new WaitForSeconds(0.2f);
        pBar.GetComponent<ProgressBar>().SetMaximum(squid.GetCurrentDistrict().maxBuildingCount);
    }

    void Start()
    {
        squid = player.GetComponent<Squid>();
        tickerScript = ticker.GetComponent<Ticker>();
        pausePanel.SetActive(false);
        popup.SetActive(false);
        achievementImage.gameObject.SetActive(false);
        StartCoroutine("wait");
    }

    public void TurnOnNews()
    {
        newsOn = true;
    }

    public void TurnOffNews()
    {
        newsOn = false;
    }
    void Update()
    {
        
        healthSlider.value = squid.GetHealth();
        healthpoints.text = squid.GetHealth().ToString();
        healthSlider.maxValue = squid.GetMaxHealth();
        if (squid.GetCurrentDistrict() == null)
        {
            districtText.text = "District: none";
            karmaText.text = "Karma: 0";
            ptsForNextSquid.text = "Karma Needed To Spawn Squiding: 0";
            currentDistrict = "None";
        }
        else
        {
            newDistrict = squid.GetCurrentDistrict().name;
            if (currentDistrict != newDistrict)
            {
                currentDistrict = newDistrict;
                PopUp("Entering " + currentDistrict);
                pBar.GetComponent<ProgressBar>().SetMaximum(squid.GetCurrentDistrict().maxBuildingCount);
            }

            pBar.GetComponent<ProgressBar>().SetCurrent(squid.GetCurrentDistrict().GetBuildingCount(), squid.GetCurrentDistrict().name);

            //districtText.text = "District: " + squid.GetCurrentDistrict().name;

            if(squid.GetCurrentDistrict().GetBuildingCount() == 0)
            {
                districtText.text = "District: " + squid.GetCurrentDistrict().name + "(ANNIHILATED)";
            }
            karmaText.text = "Karma: " + squid.GetKarma();
            ptsForNextSquid.text = "Karma Needed To Spawn Squiding: " + squid.GetPointsToNextSquid();
        }
        threatText.text = "Threat Level: " + squid.GetThreat();
        ThreatSlider.maxValue = squid.GetKarmaTillNextThreat();
        ThreatSlider.value = squid.GetKarma();

        if(squid.GetKarma() > ThreatSlider.maxValue)
        {
            ThreatSlider.value = squid.GetKarmaTillNextThreat();
        }

        //I dunno if we are still doing the show target health thing so I'll hide this for now
        /*if (targetObject != null)
        {
            if (!targetSet)
            {
                string[] sp = targetObject.name.Split('_');
                targetName = sp[0];
                sp = targetName.Split('(');
                targetName = sp[0];
                target.SetActive(true);
                targetText.text = targetName;
                if (targetObject.GetComponent<Building>() != null)
                {
                    building = targetObject.GetComponent<Building>();
                    targetSlider.maxValue = building.GetStartHealth();

                }
                targetSet = true;
            }
            if (targetObject.GetComponent<Building>() != null)
            {
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
        }*/
        if (healthSlider.value <= healthSlider.maxValue / 4)
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
        paused = false;
        menuButton.SetActive(true);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        LeaveMenuButton();
        //StartCoroutine("DelayUnPause");
    }

    /*IEnumerator DelayUnPause()
    {
        
    }*/

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

    public void PopUp(EventParam eventParam)
    {
        achievementUnlocked = true;
        popupText.text = eventParam.achievementstr_;
        popupOn = true;
        if (achievementUnlocked)
        {
            achievementImage.gameObject.SetActive(true);
        }
    }

    IEnumerator HandlePopup()
    {
        popupOn = false;
        popup.SetActive(true);
        yield return new WaitForSeconds(2);
        popup.SetActive(false);
        achievementImage.gameObject.SetActive(false);
        if (achievementUnlocked)
        {
            achievementUnlocked = false;
        }
    }
}
