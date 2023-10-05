using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BigDoorSystem : MonoBehaviour
{
    public Animator bigDoorAnim;
    private bool isInRange = false;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            isInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            isInRange = false;
        }
    }

    public void OpenDoor(InputAction.CallbackContext context)
    {
        if (context.performed && isInRange == true)
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
}
