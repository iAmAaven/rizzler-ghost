using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCellManager : MonoBehaviour
{
    LevelCell[] allLevelCells;
    public GameObject cellsParent;

    void Start()
    {
        allLevelCells = cellsParent.GetComponentsInChildren<LevelCell>();
    }
    public void UpdateStars()
    {
        Invoke("Timer", 0.1f);
    }

    void Timer()
    {
        foreach (LevelCell levelCell in allLevelCells)
        {
            if (levelCell != null)
            {
                Debug.Log(levelCell.gameObject.name);
                levelCell.UpdateBlueStars();
            }
        }
    }
}
