using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSFX : MonoBehaviour
{
    [HideInInspector] public AudioSource audioSource;
    public AudioClip[] throwSounds;

    public void PlayThrowSound()
    {
        audioSource = GetComponent<AudioSource>();
        int randomIndex = Random.Range(0, throwSounds.Length);
        audioSource.PlayOneShot(throwSounds[randomIndex]);
    }
}
