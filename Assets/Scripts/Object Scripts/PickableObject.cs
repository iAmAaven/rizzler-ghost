using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    // Hidden variables accessible from other scripts
    [HideInInspector] public bool objectGrabbed = false;
    [HideInInspector] public float followSpeed;

    // Private variables only accessible within this script
    private GameObject magnetGunObject;
    private MagnetGun magnetGun;
    private Magnet magnet;
    private Rigidbody2D rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        RefreshMagnetGun();
        // if (FindAnyObjectByType<PlayerMovement>() == true)
        // {
        //     RefreshMagnetGun();
        // }
        // else
        // {
        //     InvokeRepeating("RefreshMagnetGun", 0, 3);
        // }
    }
    void Update()
    {
        magnet = FindAnyObjectByType<Magnet>();
        if (magnet == null)
        {
            objectGrabbed = false;
        }
    }

    void FixedUpdate()
    {
        if (objectGrabbed == true && magnetGun != null)
        {
            Vector3 direction = (magnetGun.transform.position - transform.position).normalized;
            rb.MovePosition(transform.position + direction * followSpeed * Time.deltaTime);
        }
    }

    public void RefreshMagnetGun()
    {
        magnetGunObject = null;
        magnetGun = null;

        magnetGunObject = GameObject.FindWithTag("MagnetGun");

        if (magnetGunObject == null)
        {
            magnetGunObject = GameObject.FindWithTag("MagnetGunNULL");
        }
        else
        {
            magnetGun = magnetGunObject.GetComponent<MagnetGun>();
        }
    }
}
