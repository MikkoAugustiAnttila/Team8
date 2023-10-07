using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public GameObject deathParticle;
    private void OnTriggerEnter2D(Collider2D other)
    {
        stageManager.stateManagement.killsLeft--;
        GameObject particle = Instantiate(deathParticle, transform.position, quaternion.identity);
        particle.transform.parent = null;
        Destroy(gameObject);
    }

}
