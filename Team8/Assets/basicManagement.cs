using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class basicManagement : MonoBehaviour
{
    public static basicManagement basemanagement;
    public GameObject projectile;
    private GameObject pivot;
    

    private void Awake()
    {
        basemanagement = this;
        pivot = GameObject.FindGameObjectWithTag("pivot");
    }

    public void createProjectile()
    {
        GameObject newProjectile = Instantiate(projectile, pivot.transform.position, quaternion.identity);
        newProjectile.transform.parent = null;
    }
    
    
}
