using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onProjectileTouch : MonoBehaviour
{
    [SerializeField] private bool destruct;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("A");
        if (other.CompareTag("Projectile") && destruct)
        {
            Debug.Log("A");
            Destroy(other);
            basicManagement.basemanagement.createProjectile();
        }
    }
}
