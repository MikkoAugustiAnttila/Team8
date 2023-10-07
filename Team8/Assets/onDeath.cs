using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class onDeath : MonoBehaviour
{
    [SerializeField] private AudioClip deathSound;
    private void OnDestroy()
    {
        SoundManager.soundManagement.playSound(deathSound);
    }
}
