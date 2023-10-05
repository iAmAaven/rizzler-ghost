using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    public Slider musicVolumeSlider;

    void Start()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    public void VolumeChange()
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolumeSlider.value);
    }
}
