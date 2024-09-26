using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [Header("Door info")]
    public float doorWidth;
    public float doorHeight;

    [Header("References")]
    public LayerMask whatIsPlayer;

    [Header("Scenes")]
    public string sceneToLoad;


    [SerializeField] private bool toggleOldInput = false;
    private bool isPlayerInRange = false;
    private BlueStarManager blueStar;
    private PauseMenu pauseMenu;

    void Start()
    {
        pauseMenu = FindAnyObjectByType<PauseMenu>();
        blueStar = FindAnyObjectByType<BlueStarManager>();
    }
    void Update()
    {
        isPlayerInRange = Physics2D.OverlapBox(transform.position, new Vector2(doorWidth, doorHeight), 0, whatIsPlayer);

        if (Input.GetButtonDown("OpenDoor") && isPlayerInRange && toggleOldInput && !pauseMenu.isGamePaused)
        {
            if (blueStar != null && blueStar.gotBlueStar)
            {
                blueStar.starGotten = 1;
                blueStar.SaveData();
            }

            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void GoThroughDoor(InputAction.CallbackContext context)
    {
        if (!pauseMenu.isGamePaused && context.performed && isPlayerInRange && !toggleOldInput)
        {
            if (blueStar != null && blueStar.gotBlueStar)
            {
                blueStar.starGotten = 1;
                blueStar.SaveData();
            }

            SceneManager.LoadScene(sceneToLoad);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(doorWidth, doorHeight, 1));
    }
}