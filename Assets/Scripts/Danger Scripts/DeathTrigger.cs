using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{

    // THIS SCRIPT IS USED IN OBJECTS THAT HAVE A TRIGGER COLLIDER
    // WHICH KILLS PLAYER INSTANTLY. FOR EXAMPLE LAVA OR SPIKES

    void OnTriggerEnter2D(Collider2D collider)
    {
        // CHECKS IF THE GAMEOBJECT'S TAG IS "Player"
        if (collider.gameObject.tag == "Player")
        {
            // DESTROYS PLAYER GAMEOBJECT
            Destroy(collider.gameObject);


            // LOCATES THE DEATHMANAGER AND RESPAWNMANAGER SCRIPTS IN THE CURRENT SCENE
            // (THESE MANAGERS HAVE TO BE IN THE SCENE, OTHERWISE THERE ARE ERRORS)

            FindAnyObjectByType<DeathManager>().PlayerDied(); // CALLS THE PlayerDied METHOD FROM DEATHMANAGER
            FindAnyObjectByType<RespawnManager>().RespawnPlayer(); // CALLS THE RespawnPlayer METHOD FROM RESPAWNMANAGER
        }
    }
}
