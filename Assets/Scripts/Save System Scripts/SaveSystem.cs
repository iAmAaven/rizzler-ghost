using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public void SaveIntData(string saveName, int value)
    {
        PlayerPrefs.SetInt(saveName, value);
    }
    public int LoadIntData(string saveName)
    {
        return PlayerPrefs.GetInt(saveName);
    }

    public void DeleteBlueStarData()
    {
        for (int i = 1; i <= 28; i++)
        {
            PlayerPrefs.DeleteKey("BlueStar" + i);
        }
        for (int j = 1; j <= 2; j++)
        {
            PlayerPrefs.DeleteKey("BlueStarTutorial" + j);
        }
    }
}