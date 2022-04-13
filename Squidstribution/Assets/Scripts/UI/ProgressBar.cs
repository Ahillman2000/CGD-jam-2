using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    const float FADE_TIMER_MAX = 1f;


    [SerializeField] int maximum;
    [SerializeField] int current;
    [SerializeField] Image mask;
    [SerializeField] Image fadeImage;
    [SerializeField] Color fadeColour;
    [SerializeField] Text currentPercent;

    float fadeTimer;
    string currentDistrict;

    private void OnEnable()
    {
        EventManager.StartListening("BuildingDestroyed", DestructionListener);
    }

    private void OnDisable()
    {
        EventManager.StartListening("BuildingDestroyed", DestructionListener);
    }

    private void OnApplicationQuit()
    {
        Destroy(this);
        EventManager.StartListening("BuildingDestroyed", DestructionListener);
    }

    private void Awake()
    {
        fadeColour = fadeImage.color;
        fadeColour.a = 0f;
        fadeImage.color = fadeColour;
    }

    public void SetMaximum(int newMax)
    {
        maximum = newMax;
    }

    public void SetCurrent(int districtCurrent, string districtName)
    {
        current = districtCurrent;
        currentDistrict = districtName;
    }

    void Update()
    {
        
        GetCurrentFill();
        
        if (fadeColour.a > 0)
        {
            fadeTimer -= Time.deltaTime;
            if (fadeTimer < 0)
            {
                float fadeAmount = 5f;
                fadeColour.a -= fadeAmount * Time.deltaTime;
                fadeImage.color = fadeColour;
            }
        }

    }

    void GetCurrentFill()
    {
        float fillAmount = (float)current / (float)maximum;

        mask.fillAmount = fillAmount;
        currentPercent.text = currentDistrict + " " +  (int)(fillAmount * 100) + "%";
    }

    public void DestructionListener(EventParam eventParam)
    {
        if (fadeColour.a <= 0)
        {
            fadeImage.fillAmount = mask.fillAmount;
        }
        fadeColour.a = 1;
        fadeImage.color = fadeColour;
        fadeTimer = FADE_TIMER_MAX;
    }
}
