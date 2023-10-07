using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stageManager : MonoBehaviour
{
    public static stageManager stateManagement;
    public int killsLeft;
    [SerializeField] private string[] afterKills;
    [SerializeField] private string[] startDialog;
    [SerializeField] private string[] triggerDialog;
    [SerializeField] private string triggerType;
    public bool playerFired;
    [SerializeField] private string whatToDoAtEnd;
    public bool managementSignal;

    private void Awake()
    {
        stateManagement = this;
    }

    private void Start()
    {
        killsLeft = GameObject.FindGameObjectsWithTag("Enemy").Length+1;
        if (startDialog != null)
        {
            basicManagement.basemanagement.DialogChunk(false, startDialog);
        }
    }

    private void Update()
    {
        if (killsLeft == 1 && afterKills != null)
        {
            basicManagement.basemanagement.DialogChunk(true, afterKills);
            killsLeft--;
        }

        if (triggerType != null)
        {
            if (triggerType == "Fire" && playerFired == true)
            {
                basicManagement.basemanagement.DialogChunk(true, triggerDialog);
                triggerType = null;
            }
        }
        

        if (managementSignal == true && whatToDoAtEnd != null)
        {
            managementSignal = false;
            if (whatToDoAtEnd == "returnToMenuGlitch1")
            {
                basicManagement.basemanagement.lastBug = "TutorialGlitch";
                basicManagement.basemanagement.ChangeToScene("LevelChanger");
            }
            if (whatToDoAtEnd == "returnToMenuReset")
            {
                basicManagement.basemanagement.lastBug = "";
                basicManagement.basemanagement.ChangeToScene("LevelChanger");
            }
        }
    }
}
