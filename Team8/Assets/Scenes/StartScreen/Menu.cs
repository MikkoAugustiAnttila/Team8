using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Playbutton()
    {
        /*
        SceneManager.LoadScene("LevelChanger", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        */
        basicManagement.basemanagement.ChangeToScene("LevelChanger");
    }
    public void OnQuitButton()
    {
        Application.Quit();
    }

}
