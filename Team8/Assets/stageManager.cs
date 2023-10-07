using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [SerializeField] private int maxShots;
    public int shotsLeft;
    [SerializeField] private TextMeshProUGUI shotCounter;
    

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

        shotCounter = GameObject.FindGameObjectWithTag("shotCounter").GetComponent<TextMeshProUGUI>();
        shotCounter.enabled = true;
        shotsLeft = maxShots;
    }

    private void Update()
    {
        shotCounter.text = "Shots remaining: " + shotsLeft + "/" + maxShots;
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

        if (shotsLeft == 0 && maxShots != 0 && killsLeft <= 2)
        {
            managementSignal = true;
            whatToDoAtEnd = "resetScene";
        }
        

        if (managementSignal == true && whatToDoAtEnd != null)
        {
            managementSignal = false;
            shotCounter.enabled = false;
            shotsLeft = 999;
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
            if (whatToDoAtEnd == "resetScene")
            {
                basicManagement.basemanagement.ChangeToScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
