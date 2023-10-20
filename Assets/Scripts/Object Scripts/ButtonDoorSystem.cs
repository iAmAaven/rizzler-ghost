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
        if (context.performed && isInRange == true)
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
}
