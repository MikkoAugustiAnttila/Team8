using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderTrigger : MonoBehaviour
{
    [SerializeField] private string triggerName;
    [SerializeField] private string sceneName;
    [SerializeField] private float delay;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            other.gameObject.transform.position = new Vector3(100,100,100);
            stageManager.stateManagement.externalTrigger(triggerName, sceneName, delay);
        }
    }
}
