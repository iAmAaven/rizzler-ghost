using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public AudioMixer masterMixer;

    void Start()
    {
        masterMixer.SetFloat("musicVolume", Mathf.Log10(PlayerPrefs.GetFloat("musicVolume")) * 20);
        masterMixer.SetFloat("sfxVolume", Mathf.Log10(PlayerPrefs.GetFloat("sfxVolume")) * 20);
    }
}
