using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Animator doorAnim;
    public Animator leverAnim;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Magnet")
        {
            Animate();
            GameObject collided = collision.gameObject;
            Magnet collidedMagnet = collided.GetComponent<Magnet>();
            collidedMagnet.grabbed = true;

            collidedMagnet.Invoke("LetGoOfLever", collidedMagnet.letGoTimer);
        }
    }
    public void Animate()
    {
        leverAnim.SetTrigger("Activate");
        doorAnim.SetTrigger("Activate");
    }
}
