using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPlate : MonoBehaviour
{
    public float doorCloseTimer = 0f;
    public Animator anim;
    public AudioSource doorClosingSFX;
    public AudioSource doorOpeningSFX;

    [SerializeField] private bool canBeOpenedWithMagnet = false;
    [SerializeField] private bool closeDoorsAfter = false;

    private bool activated = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (activated == false)
        {
            if ((collider.gameObject.tag == "LightBox" && canBeOpenedWithMagnet == false)
            || (collider.gameObject.tag == "Magnet" && canBeOpenedWithMagnet == true))
            {
                activated = true;
                if (doorClosingSFX.isPlaying)
                {
                    doorClosingSFX.Stop();
                }
                doorOpeningSFX.Play();
                anim.SetTrigger("Activate");

                if (closeDoorsAfter == true)
                {
                    Invoke("CloseDoorsTimer", doorCloseTimer);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "LightBox" && canBeOpenedWithMagnet == false)
        {
            CloseDoorsTimer();
        }
    }

    void CloseDoorsTimer()
    {
        doorClosingSFX.Play();
        anim.SetTrigger("Deactivate");
        activated = false;
    }
}
