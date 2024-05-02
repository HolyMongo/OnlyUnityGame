using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audiosource;
    public void PlaySound()
    {
        audiosource.clip = audioClip;
        audiosource.Play();
    }
}
