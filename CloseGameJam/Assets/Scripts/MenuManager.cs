using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject creditsPanel;
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

    public void ExitButtonPressed()
    {
        Application.Quit();
    }

}
