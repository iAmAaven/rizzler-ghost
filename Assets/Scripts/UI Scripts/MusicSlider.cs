using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    public Slider musicSlider;
    public AudioMixer audioMixer;

    void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }
        else
        {
            musicSlider.value = 1;
        }
    }

    public void VolumeChange()
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(musicSlider.value) * 20);
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }
}
