using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManagement;
    public AudioClip[] BackgroundMusic;

    private void Awake()
    {
        soundManagement = this;
    }

    public void playSound(AudioClip audio)
    {
        AudioSource.PlayClipAtPoint(audio, transform.position);
    }
}
