using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stageManager : MonoBehaviour
{
    public static stageManager stateManagement;
    public int killsLeft;
    [SerializeField] private string[] afterKills;

    private void Awake()
    {
        stateManagement = this;
    }

    private void Start()
    {
        killsLeft = GameObject.FindGameObjectsWithTag("Enemy").Length+1;
    }

    private void Update()
    {
        if (killsLeft == 1 && afterKills != null)
        {
            basicManagement.basemanagement.DialogChunk(afterKills);
            killsLeft--;
        }
    }
}
