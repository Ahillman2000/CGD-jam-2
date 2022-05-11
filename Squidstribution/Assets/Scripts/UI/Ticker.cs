using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticker : MonoBehaviour
{
    [SerializeField] private TickerText tickerTextPrefab;
    [Range(1f, 10f)] [SerializeField] private float itemDuration = 3f;
    [SerializeField] private string[] fillerItems;
    private string previousItem;

    private float width;
    private float pixelsPerSecond;
    TickerText currentText;

    private bool newsing = true;

    // Start is called before the first frame update
    void Start()
    {
        width = GetComponent<RectTransform>().rect.width;
        pixelsPerSecond = width / itemDuration;
        //StartCoroutine(DelayNews());
    }

    // Update is called once per frame
    void Update()
    {
        if (!newsing)
        {
            if (currentText.GetXPosition <= -currentText.GetWidth)
            {
                StartCoroutine(DelayNews());
            }
        }
    }

    void AddTickerText(string message)
    {
        currentText = Instantiate(tickerTextPrefab, transform);
        currentText.Initialize(width, pixelsPerSecond, message);
        previousItem = message;
    }

    IEnumerator DelayNews()
    {
        newsing = true;
        yield return new WaitForSeconds(1f);
        bool searching = true;
        while (searching)
        {

            string current_text = fillerItems[Random.Range(0, fillerItems.Length)];
            if (previousItem != current_text)
            {
                AddTickerText(current_text);
                searching = false;
            }
        }
        
        newsing = false;
    }

    public void GenOneNews()
    {
        StartCoroutine(DelayNews());
    }
}
