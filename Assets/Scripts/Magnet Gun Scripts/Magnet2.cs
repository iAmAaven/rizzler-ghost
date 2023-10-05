using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Magnet2 : MonoBehaviour
{
    public float speed;
    public float lifespan;
    private Transform floatingPos;
    // private CircleCollider2D circleCollider;
    private Rigidbody2D rb;
    private MagnetGun2 magnetGun2;
    private Vector2 mousePos;
    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        if (magnetGun2 == null)
        {
            magnetGun2 = FindAnyObjectByType<MagnetGun2>();
        }

        lifespan = magnetGun2.magnetLifeTimer;
        floatingPos = magnetGun2.targetPos;
        speed = magnetGun2.magnetReturnSpeed;

        Invoke("MagnetLifespan", lifespan);
    }

    void FixedUpdate()
    {
        // rb.MovePosition(new Vector3(0, 0, 0) * Time.deltaTime * speed);
        if (floatingPos == null)
        {
            Destroy(gameObject);
            return;
        }
        mousePos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mainCamera.nearClipPlane));
        Vector3 direction = mouseWorldPosition - transform.position;



        // Vector3 direction = (floatingPos.transform.position - transform.position).normalized;
        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        magnetGun2.hasBeenShot = false;
        Destroy(gameObject);
    }

    void MagnetLifespan()
    {
        magnetGun2.hasBeenShot = false;
        Destroy(gameObject);
    }
}