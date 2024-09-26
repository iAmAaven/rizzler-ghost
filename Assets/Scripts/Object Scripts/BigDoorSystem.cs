using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BigDoorSystem : MonoBehaviour
{
    public Animator bigDoorAnim;
    public Animator buttonAnim;
    private bool isInRange = false;
    [SerializeField] private bool toggleOldInput = false;
    private PauseMenu pauseMenu;

    void Start()
    {
        pauseMenu = FindAnyObjectByType<PauseMenu>();
    }

    void Update()
    {
        if (toggleOldInput && Input.GetButtonDown("OpenDoor") && !pauseMenu.isGamePaused && isInRange)
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
        if (toggleOldInput == false && context.performed && isInRange == true && !pauseMenu.isGamePaused)
        {
            AnimateDoor();
        }
    }

    void AnimateDoor()
    {
        if (bigDoorAnim.GetBool("IsOpening") == false)
        {
            bigDoorAnim.SetBool("IsOpening", true);
            bigDoorAnim.SetBool("IsClosing", false);
            return;
        }
        else if (bigDoorAnim.GetBool("IsClosing") == false)
        {
            bigDoorAnim.SetBool("IsClosing", true);
            bigDoorAnim.SetBool("IsOpening", false);
        }
    }
}
