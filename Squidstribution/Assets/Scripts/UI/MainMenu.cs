using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text startText, optionText, exitText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnStart()
    {
        startText.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;
    }

    public void LeaveStart()
    {
        startText.GetComponent<Text>().fontStyle = FontStyle.Bold;
    }

    public void OnOptions()
    {
        optionText.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;
    }

    public void LeaveOptions()
    {
        optionText.GetComponent<Text>().fontStyle = FontStyle.Bold;
    }

    public void OnExit()
    {
        exitText.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;
    }

    public void LeaveExit()
    {
        exitText.GetComponent<Text>().fontStyle = FontStyle.Bold;
    }
}
