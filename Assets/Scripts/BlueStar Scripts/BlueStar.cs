using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueStar : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            BlueStarManager bsManager = FindAnyObjectByType<BlueStarManager>();
            StarUI starUI = FindAnyObjectByType<StarUI>();
            starUI.BlueStarGotten();

            bsManager.gotBlueStar = true;
            bsManager.blueStarSFX.PlayOneShot(bsManager.blueStarSFX.clip);
            Destroy(gameObject);
        }
    }
}