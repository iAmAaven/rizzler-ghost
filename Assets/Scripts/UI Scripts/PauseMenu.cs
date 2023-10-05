using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("References")]
    public GameObject pauseMenuCanvas;

    [HideInInspector]
    public bool isGamePaused = false;


    public void PauseGame(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isGamePaused == true)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        isGamePaused = true;
        pauseMenuCanvas.SetActive(true);
    }
    public void Continue()
    {
        Time.timeScale = 1;
        isGamePaused = false;
        pauseMenuCanvas.SetActive(false);
    }
    public void GoToMenu()
    {
        Time.timeScale = 1;
        isGamePaused = false;
        pauseMenuCanvas.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
