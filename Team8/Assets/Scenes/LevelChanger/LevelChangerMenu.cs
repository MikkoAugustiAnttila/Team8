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
        if (basicManagement.basemanagement.lastBug == "NewGame")
        {
            basicManagement.basemanagement.ChangeToScene("TutorialVariant 1");
        }
        else if (basicManagement.basemanagement.lastBug == "TutorialGlitch")
        {
            basicManagement.basemanagement.ChangeToScene("TutorialVariant 2");
        }
        else
        {
            basicManagement.basemanagement.ChangeToScene("TutorialVariant 3");
        }
    }
    public void SetLevelActive(int value)
    {
        _buttons[value+1].interactable = true;
    }
    

}