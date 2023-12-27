using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextWorldCheck : MonoBehaviour
{
    public int firstRoomNumber;
    public int lastRoomNumber;
    public GameObject lockImage;
    public GameObject arrowImage;
    private Button button;
    private bool allLevelsCompleted = false;

    void OnEnable()
    {
        button = GetComponent<Button>();
        button.enabled = false;

        for (int i = firstRoomNumber; i <= lastRoomNumber; i++)
        {
            if (PlayerPrefs.GetInt("BlueStar" + i) == 0)
            {
                allLevelsCompleted = false;
                break;
            }
            else
            {
                allLevelsCompleted = true;
            }
        }

        if (allLevelsCompleted == true)
        {
            arrowImage.SetActive(true);
            lockImage.SetActive(false);
            button.enabled = true;
        }
        else
        {
            arrowImage.SetActive(false);
            lockImage.SetActive(true);
            button.enabled = false;
        }
    }
}
