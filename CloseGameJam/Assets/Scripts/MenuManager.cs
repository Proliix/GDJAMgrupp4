using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject creditsPanel;
    [SerializeField] GameObject helpPanel;
    private void Start()
    {
        int hasPlayed = PlayerPrefs.GetInt("HasPlayed", 0);
        if(hasPlayed == 1)
        {
            CloseHelpMenu();
        }
        PlayerPrefs.SetInt("HasPlayed", 1);
    }
    public void PlayButtonPressed()
    {
        SceneManager.LoadScene("LucasTest");
    }
    public void CreditsButtonPressed()
    {
        creditsPanel.SetActive(true);
    }

    public void CloseCreditsPanel()
    {
        creditsPanel.SetActive(false);
    }

    public void CloseHelpMenu()
    {
        helpPanel.SetActive(false);
    }

    public void OpenHelpMenu()
    {
        helpPanel.SetActive(true);
    }

    public void ExitButtonPressed()
    {
        //Application.Quit();
    }

}
