using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITextManager : MonoBehaviour
{
    public TextMeshProUGUI deathAmount;
    public TextMeshProUGUI magnetAmount;

    public int magnets;

    public void UpdateDeaths(int playerDeaths)
    {
        deathAmount.text = "" + playerDeaths;
    }

    public void UpdateMagnets(int magnetCount)
    {
        magnets = magnetCount;
        magnetAmount.text = "" + magnets;
    }
}
