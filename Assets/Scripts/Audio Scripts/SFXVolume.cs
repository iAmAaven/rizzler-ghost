using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXVolume : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        audioSource.volume = PlayerPrefs.GetFloat("sfxVolume");
    }
}
