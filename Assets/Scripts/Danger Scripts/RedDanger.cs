using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDanger : MonoBehaviour
{
    private MagnetGun2 magnetGun2;
    void Awake()
    {
        magnetGun2 = FindAnyObjectByType<MagnetGun2>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Magnet")
        {
            magnetGun2.hasBeenShot = false;
            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.tag == "MoveableObject"
            || collider.gameObject.tag == "LightBox")
        {
            FindAnyObjectByType<ObjectRespawnManager>().RespawnObjects();
        }
        else if (collider.gameObject.tag == "Player")
        {
            FindAnyObjectByType<DeathManager>().PlayerDied();
            FindAnyObjectByType<RespawnManager>().RespawnPlayer();
            Destroy(collider.gameObject);
        }
    }
}
