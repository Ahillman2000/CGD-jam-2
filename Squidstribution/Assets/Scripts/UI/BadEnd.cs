using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BadEnd : MonoBehaviour
{
    [SerializeField] private TMP_Text retryText, menuText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Retry()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnRetry()
    {
        retryText.fontStyle = FontStyles.Underline | FontStyles.Bold;
    }

    public void LeaveRetry()
    {
        retryText.fontStyle = FontStyles.Bold;
    }

    public void OnMainMenu()
    {
        menuText.fontStyle = FontStyles.Underline | FontStyles.Bold;
    }

    public void LeaveMainMenu()
    {
        menuText.fontStyle = FontStyles.Bold;
    }
}
