using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXSlider : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("sfxVolume");
    }
    public void VolumeChange()
    {
        PlayerPrefs.SetFloat("sfxVolume", slider.value);
    }
}
