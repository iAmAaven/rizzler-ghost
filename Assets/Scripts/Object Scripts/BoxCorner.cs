using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCorner : MonoBehaviour
{
    public AudioSource boxSFX;
    public AudioClip[] boxClonkSFXs;
    void Start()
    {
        boxSFX.volume = 0;
        Invoke("UnmuteAudio", 1f);
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject collidedObject = collider.gameObject;
        if (collidedObject.tag == "MoveableObject"
        || collidedObject.tag == "Ground"
        || collidedObject.tag == "Wall"
        || collidedObject.tag == "Particle")
        {
            int randomNumber = Random.Range(0, boxClonkSFXs.Length);

            if (boxSFX.isPlaying == false)
            {
                boxSFX.clip = boxClonkSFXs[randomNumber];
                boxSFX.Play();
            }
        }
    }

    void UnmuteAudio()
    {
        boxSFX.volume = 0.5f;
    }
}
