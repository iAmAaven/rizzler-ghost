using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonDoorSystem : MonoBehaviour
{
    public Animator doorAnim;
    public Animator buttonAnim;
    private bool isInRange = false;
    private bool isOpen = false;
    private bool toggleOldInput = false;
    private PauseMenu pauseMenu;

    void Start()
    {
        pauseMenu = FindAnyObjectByType<PauseMenu>();
    }

    void Update()
    {
        if (!pauseMenu.isGamePaused && Input.GetButtonDown("OpenDoor") && isInRange && toggleOldInput)
        {
            AnimateDoor();
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            buttonAnim.SetTrigger("InRange");
            isInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            buttonAnim.SetTrigger("OutOfRange");
            isInRange = false;
        }
    }

    public void OpenDoor(InputAction.CallbackContext context)
    {
        if (!pauseMenu.isGamePaused && context.performed && isInRange == true && toggleOldInput == false)
        {
            AnimateDoor();
        }
    }

    void AnimateDoor()
    {
        if (isOpen == true)
        {
            doorAnim.SetTrigger("Close");
            isOpen = false;
        }
        else
        {
            doorAnim.SetTrigger("Open");
            isOpen = true;
        }
    }
}
