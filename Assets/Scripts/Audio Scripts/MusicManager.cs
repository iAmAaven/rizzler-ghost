using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public AudioSource music;

    void Update()
    {
        music.volume = PlayerPrefs.GetFloat("musicVolume");
    }
}
