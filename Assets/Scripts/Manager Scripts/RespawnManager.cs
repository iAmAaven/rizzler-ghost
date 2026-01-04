using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{

    // THIS SCRIPT IS USED IN AN EMPTY GAMEOBJECT CALLED "RESPAWNMANAGER"
    // KEEPS TRACK OF THE PLAYER'S RESPAWNPOINTS AND STARTPOINT 


    [Header("Respawn time")]
    public float timeToRespawn = 2f;

    [Header("References")]
    public Transform startPoint;
    public GameObject playerPrefab;


    void Awake()
    {
        // CALLS THE FindAllObjectsInScene METHOD WHEN THIS SCRIPT/GAMEOBJECT IS CREATED
        // FindAllObjectsInScene();
    }


    // RESPAWNS PLAYER WHEN CALLED
    public void RespawnPlayer()
    {
        // CALLS THE IEnumerator "RespawnTimer"
        StartCoroutine(RespawnTimer());
    }

    private IEnumerator RespawnTimer()
    {
        // WAITS THE AMOUNT OF timeToRespawn FLOAT VALUE
        yield return new WaitForSeconds(timeToRespawn);

        // MAKES A NEW PLAYER GAMEOBJECT AND PUTS IT IN THE POSITION OF THE LEVEL STARTING POINT
        Instantiate(playerPrefab, new Vector3(startPoint.position.x,
            startPoint.position.y, startPoint.position.z), Quaternion.identity);

        // CALLS THE FindAllObjectsInScene METHOD
        FindAllObjectsInScene();
    }

    // THIS METHOD IS USED BY THE NEW INPUT SYSTEM AND IS CALLED EVERY TIME "R" IS PRESSED
    public void RestartLevel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // USES THE SCENEMANAGER TO LOAD THE CURRENT SCENE AGAIN, RESTARTING EVERYTHING
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    // LOCATES EVERY GAMEOBJECT WITH THE PickableObject SCRIPT ATTACHED TO IT
    public void FindAllObjectsInScene()
    {
        PickableObject[] pickableObjects = FindObjectsByType<PickableObject>(FindObjectsSortMode.None);

        // CALLS THE RefreshPullGun METHOD FROM EVERY PickableObject IN THE SCENE
        foreach (PickableObject objects in pickableObjects)
        {
            // RefreshPullGun METHOD LOCATES THE PULLGUN GAMEOBJECT IN THE SCENE
            // AGAIN AFTER IT HAS BEEN DESTROYED DUE TO PLAYER'S DEATH
            objects.GetComponent<PickableObject>().RefreshMagnetGun();
        }
    }
}
