using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onStart : MonoBehaviour
{
    [SerializeField] private AudioClip startSound;
    private void Start()
    {
        SoundManager.soundManagement.playSound(startSound);
    }
}
