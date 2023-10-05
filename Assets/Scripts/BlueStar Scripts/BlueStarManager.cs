using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueStarManager : MonoBehaviour
{
    public string blueStarSaveName;
    private SaveSystem saveSystem;
    private BlueStar blueStar;
    [HideInInspector] public bool gotBlueStar = false;
    [HideInInspector] public int starGotten;
    [HideInInspector] public AudioSource blueStarSFX;

    void Start()
    {
        blueStarSFX = GetComponent<AudioSource>();
        saveSystem = FindAnyObjectByType<SaveSystem>();
        blueStar = GetComponentInChildren<BlueStar>();

        if (saveSystem.LoadIntData(blueStarSaveName) == 1)
        {
            Destroy(blueStar.gameObject);
        }
    }
    void Update()
    {
        blueStarSFX.volume = PlayerPrefs.GetFloat("sfxVolume");
    }
    public void SaveData()
    {
        saveSystem.SaveIntData(blueStarSaveName, starGotten);
    }
}
