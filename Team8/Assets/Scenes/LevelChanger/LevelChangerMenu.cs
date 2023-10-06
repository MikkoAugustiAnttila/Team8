using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChangerMenu : MonoBehaviour
{
    public Button[] _buttons;
    private void Start()
    {
        //SetLevelActive(2);
        
    }
    public void Level1()
    {
        SceneManager.LoadScene("Sling");
    }
    public void SetLevelActive(int value)
    {
        _buttons[value+1].interactable = true;
    }
    

}