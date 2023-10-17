using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    // THIS SCRIPT IS USED IN AN EMPTY GAMEOBJECT CALLED "DEATHMANAGER"
    // KEEPS TRACK OF THE AMOUNT OF PLAYER DEATHS IN CURRENT SCENE AND CHECKS WHAT TO DO WITH THE "BlueStar"

    private BlueStarManager blueStarManager;

    public void PlayerDied()
    {
        // LOCATES THE SCRIPTS USED TO CONTROL BlueStar PROGRESS
        BlueStar blueStar = FindAnyObjectByType<BlueStar>();

        blueStarManager = FindAnyObjectByType<BlueStarManager>();
        StarUI starUI = FindAnyObjectByType<StarUI>();

        if (starUI == null)
            return;

        starUI.BlueStarLost();

        // THE PURPOSE OF THE NEXT IF STATEMENT IS TO CHECK IF THE PLAYER HAS ALREADY GOTTEN THE BlueStar
        // IN THE CURRENT SCENE. IF THEY HAVE, THE BLUESTAR WILL NOT BE SPAWNED. OR RATHER, IT WILL BE DESTROYED.


        // LOADS DATA FROM SAVESYSTEM SCRIPT IN THE CURRENT SCENE. THE DATA BEING LOADED IS THE CURRENT
        // SCENE'S BLUESTAR'S SAVE FILE THAT CHECKS WHETHER BlueStar HAS BEEN GOTTEN ON THE LEVEL BEFORE
        // (1 = true) AND (0 = false)
        // "blueStarSaveName" IS BASICALLY "BlueStar#" WHERE THE # REPRESENTS A NUMBER WHICH IS BASED ON THE
        // CURRENT SCENE (FOR EXAMPLE THE SCENE "Room1" HAS "BlueStar1", AND "Room2" HAS "BlueStar2" AND SO ON)
        // THIS STATEMENT IS TRUE IF BlueStar HAS ALREADY BEEN GOTTEN IN THE CURRENT LEVEL BEFORE
        if (FindAnyObjectByType<SaveSystem>().LoadIntData(blueStarManager.blueStarSaveName) == 1)
        {
            if (blueStar != null) // CHECKS WHETHER OR NOT A BlueStar SCRIPT IS FOUND IN THE CURRENT SCENE
            {
                Destroy(blueStar); // IF IT IS FOUND, IT DESTROYS IT
            }

            // BECAUSE WE DON'T WANT TO RESET THE blueStarStats WHICH WILL DELETE SOME OF THE
            // PLAYER'S PROGRESS IF THE PLAYER HAS ALREADY GOTTEN THE BlueStar BEFORE, WE RETURN IT WITHOUT
            // COMPLETING THE RESET.
            return;
        }

        // IF THE BlueStar HAS NOT BEEN GOTTEN IN THE LEVEL BEFORE, THE CODE CHECKS IF THE BlueStar HAS BEEN
        // GOTTEN DURING THE CURRENT RUN OF THE LEVEL
        // IF THE PLAYER DIES BEFORE GETTING THE BlueStar THIS STATEMENT IS TRUE
        if (blueStar != null)
        {
            Destroy(blueStar.gameObject); // DESTROYS THE OBJECT THAT HAS "BlueStar" SCRIPT ATTACHED TO IT
            ResetBlueStarStats(); // CALLS A METHOD WHICH RESETS BlueStar PROGRESS IN THE CURRENT SCENE
        }
        else // IF THE PLAYER GOT THE BlueStar DURING THE CURRENT RUN BUT DIED AFTER GETTING IT...
        {
            ResetBlueStarStats(); //... WE RESET THE BlueStarStats
        }
    }

    void ResetBlueStarStats()
    {
        // SETS THE VALUE TO 0, MEANING FALSE, OF THE INTEGER WHICH GOES TO THE SAVEFILE
        blueStarManager.starGotten = 0;
        // SETS THE BOOL gotBlueStar TO FALSE WHICH IS USED BY THE "Door" SCRIPT
        blueStarManager.gotBlueStar = false;
        // CALLS THE "SaveData" METHOD FROM BlueStarManager.
        blueStarManager.SaveData();
    }
}