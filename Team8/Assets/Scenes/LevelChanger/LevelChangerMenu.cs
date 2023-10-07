using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChangerMenu : MonoBehaviour
{
    public Button[] _buttons;
    private int _progression;
    private void Start()
    {
        _progression = basicManagement.basemanagement.progression;
        while (_progression > 0)
        {
            SetLevelActive(_progression);
            _progression--;

        }

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
            SetLevelActive(2);
            basicManagement.basemanagement.ChangeToScene("TutorialVariant 3");
            
        }
        
    }
    public void Level2()
    {
        SetLevelActive(3);
        basicManagement.basemanagement.ChangeToScene("Stage 1");
        
    }
    public void Level3()
    {
        basicManagement.basemanagement.ChangeToScene("Stage 2");
    }
    public void SetLevelActive(int value)
    {
        _buttons[value-1].interactable = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            SetLevelActive(2);
            SetLevelActive(3);
            SetLevelActive(4);
        }
    }

}