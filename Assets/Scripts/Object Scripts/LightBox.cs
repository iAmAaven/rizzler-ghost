using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBox : MonoBehaviour
{
    private Magnet2 magnetBall;
    private Rigidbody2D rb;
    private bool magnetFound = false;
    private float returnSpeed;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void CheckForMagnets()
    {
        magnetBall = FindAnyObjectByType<Magnet2>();
        if (magnetBall != null)
        {
            magnetFound = true;
            returnSpeed = magnetBall.speed;
        }
        else
        {
            magnetFound = false;
        }
    }

    void FixedUpdate()
    {
        CheckForMagnets();
        if (magnetFound == true)
        {
            Vector3 direction = (magnetBall.transform.position - transform.position).normalized;
            rb.MovePosition(transform.position + direction * returnSpeed * Time.deltaTime);
        }
    }
}
