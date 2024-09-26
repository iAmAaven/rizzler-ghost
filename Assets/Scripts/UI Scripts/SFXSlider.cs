using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SFXSlider : MonoBehaviour
{
    public Slider sfxSlider;
    public AudioMixer audioMixer;

    void Start()
    {
        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        }
        else
        {
            sfxSlider.value = 1;
        }
    }
    public void VolumeChange()
    {
        audioMixer.SetFloat("sfxVolume", Mathf.Log10(sfxSlider.value) * 20);
        PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
    }
}
