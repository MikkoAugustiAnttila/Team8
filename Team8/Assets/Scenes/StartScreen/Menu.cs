using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject CreditsPanel;
    public void Playbutton()
    {
        /*
        SceneManager.LoadScene("LevelChanger", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        */
        basicManagement.basemanagement.ChangeToScene("LevelChanger");
    }
    public void Creditsbutton()
    {
        CreditsPanel.SetActive(true);
    }
    public void OnQuitButton()
    {
        Application.Quit();
    }
    public void CloseButton()
    {
        CreditsPanel.SetActive(false);
    }

}
