using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlatform : MonoBehaviour
{
    private Animator anim;
    private AudioSource bounceSFX;

    void Start()
    {
        bounceSFX = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        bounceSFX.volume = PlayerPrefs.GetFloat("sfxVolume");
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player"
        || collision.gameObject.tag == "MoveableObject"
        || collision.gameObject.tag == "Magnet")
        {
            bounceSFX.Play();
            anim.SetTrigger("Bounce");
        }
    }
}
