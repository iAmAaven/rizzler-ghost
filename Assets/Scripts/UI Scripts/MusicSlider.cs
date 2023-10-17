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
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    public void VolumeChange()
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(musicSlider.value) * 20);
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }
}
