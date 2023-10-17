using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelCell : MonoBehaviour
{
    public string saveDataName;
    public GameObject starImage;
    private TextMeshProUGUI numberText;

    void Start()
    {
        numberText = GetComponentInChildren<TextMeshProUGUI>();
        UpdateBlueStars();
    }

    public void UpdateBlueStars()
    {
        if (saveDataName == "")
            return;

        SaveSystem saveSystem = FindAnyObjectByType<SaveSystem>();
        int saveData = saveSystem.LoadIntData(saveDataName);


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
}
