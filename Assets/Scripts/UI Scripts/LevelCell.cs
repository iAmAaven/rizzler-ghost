using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCell : MonoBehaviour
{
    public string saveDataName;
    public int worldNumber = 1;
    public GameObject starImage;
    public GameObject lockImage;
    public bool isTutorialCell = false;
    private TextMeshProUGUI numberText;
    private Button button;

    void Start()
    {
        numberText = GetComponentInChildren<TextMeshProUGUI>();
        button = GetComponent<Button>();
        UpdateBlueStars();
    }

    public void UpdateBlueStars()
    {
        if (saveDataName == "")
            return;


        SaveSystem saveSystem = FindAnyObjectByType<SaveSystem>();
        int saveData = saveSystem.LoadIntData(saveDataName);

        if (isTutorialCell == false)
        {
            if (saveSystem.LoadIntData("BlueStarTutorial" + worldNumber) == 1)
            {
                lockImage.SetActive(false);
                numberText.enabled = true;
                button.enabled = true;

                if (starImage != null)
                {
                    if (saveData == 1)
                    {
                        starImage.SetActive(true);
                        numberText.enabled = false;
                    }
                    else
                    {
                        starImage.SetActive(false);
                        numberText.enabled = true;
                    }
                }
            }
            else
            {
                starImage.SetActive(false);
                lockImage.SetActive(true);
                numberText.enabled = false;
                button.enabled = false;
            }
        }
        else
        {
            if (saveSystem.LoadIntData("BlueStarTutorial" + worldNumber) == 1)
            {
                starImage.SetActive(true);
                numberText.enabled = false;
            }
            else
            {
                starImage.SetActive(false);
                numberText.enabled = true;
            }
        }
    }
}
