using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    // Public variables

    [Header("Magnet properties")]
    public float returnSpeed = 10f;
    public float letGoTimer;
    public float breakAnimLength = 0.25f;

    [Header("References")]
    public AudioSource magnetSFX;

    // Hidden variable accessible from other scripts
    [HideInInspector] public bool grabbed = false;

    // Private variables
    private MagnetGun magnetGun;
    private CircleCollider2D circleCollider;
    private Rigidbody2D rb;
    private Transform grabbedObject = null;
    private Animator anim;

    void Awake()
    {
        // Get references to components and adjust settings based on MagnetGun
        anim = GetComponent<Animator>();
        magnetSFX = GetComponent<AudioSource>();
        circleCollider = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        magnetGun = FindAnyObjectByType<MagnetGun>();
        letGoTimer = magnetGun.returnTimer;
        returnSpeed = magnetGun.magnetReturnSpeed;
    }

    void Update()
    {
        magnetSFX.volume = PlayerPrefs.GetFloat("sfxVolume");

        // If an object is currently grabbed
        if (grabbedObject != null)
        {
            // Lets go of the object after a delay
            Invoke("LetGoOfObject", letGoTimer);
        }

        // If the associated MagnetGun script is missing
        if (magnetGun == null)
        {
            grabbed = false;

            // Stops all motion, plays break animation, and destroys the object
            StopAllMotion();
        }
    }

    void FixedUpdate()
    {
        // If the magnet is currently grabbed and a MagnetGun is available
        if (grabbed == true)
        {
            // Disable collider and move the magnet towards the MagnetGun
            circleCollider.enabled = false;
            if (magnetGun != null)
            {
                Vector3 direction = (magnetGun.transform.position - transform.position).normalized;
                rb.MovePosition(transform.position + direction * returnSpeed * Time.deltaTime);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle collision with different object types

        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Ground")
        {
            magnetGun.hasBeenShot = false;
            StopAllMotion();
            return;
        }
        // If the collision is with a moveable object (or a light box, which is a different type of moveable object)
        else if (collision.gameObject.tag == "MoveableObject" || collision.gameObject.tag == "LightBox")
        {
            // Assigns the transform of the collided gameObject to grabbedObject variable
            grabbedObject = collision.gameObject.transform;

            // Gets PickableObject script from the grabbedObject
            PickableObject pickableObject = grabbedObject.GetComponent<PickableObject>();

            // If the pickableObject script is disabled, magnet will break and be destroyed
            if (pickableObject.enabled == false)
            {
                magnetGun.hasBeenShot = false;
                grabbedObject.GetComponent<PickableObject>().objectGrabbed = false;

                StopAllMotion();
                return;
            }

            // Updates the grabbedObject's properties and prepares it to be moved towards the MagnetGun
            grabbedObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            pickableObject.objectGrabbed = true;
            pickableObject.followSpeed = returnSpeed;

            // Updates the properties and prepares the magnet to be moved towards the MagnetGun
            rb.velocity = Vector2.zero;
            circleCollider.enabled = false;
            rb.gravityScale = 0;
            grabbed = true;

            Debug.Log(collision.gameObject + " hit.");

            // Lets go of the object after a delay
            Invoke("LetGoOfObject", letGoTimer);
        }
        else if (collision.gameObject.tag == "IgnoreCollision")
        {
            return;
        }
        else
        {
            // If the collision is with an unrecognized object type, the magnet will break
            grabbed = false;
            Debug.Log(collision.gameObject);
            magnetGun.hasBeenShot = false;

            if (grabbedObject != null)
            {
                grabbedObject.GetComponent<PickableObject>().objectGrabbed = false;
            }

            StopAllMotion();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Handles trigger collision with different object types

        // If the trigger collision is not with an object with the "BlueStar" tag, the magnet will break
        if (collider.gameObject.tag != "BlueStar" && collider.gameObject.tag != "IgnoreCollision")
        {
            grabbed = false;
            if (grabbedObject != null)
            {
                grabbedObject.GetComponent<PickableObject>().objectGrabbed = false;
            }
            magnetGun.hasBeenShot = false;

            StopAllMotion();
        }
    }

    // Lets go of the currently grabbed object
    void LetGoOfObject()
    {
        Rigidbody2D objectRigidbody = grabbedObject.GetComponent<Rigidbody2D>();

        // If a MagnetGun is present, apply force to the object towards the gun
        if (magnetGun != null && grabbedObject.GetComponent<PickableObject>().objectGrabbed == true)
        {
            magnetGun.hasBeenShot = false;
            Vector3 direction = (magnetGun.transform.position - transform.position).normalized;
            objectRigidbody.velocity = direction * returnSpeed;
            // grabbedObject.GetComponent<PickableObject>().objectGrabbed = false;
        }
        else if (magnetGun == null)
        {
            // If the MagnetGun is missing, just stop the object's velocity and destroy the magnet
            objectRigidbody.velocity = Vector2.zero;

            Destroy(gameObject);
        }

        grabbed = false;

        // Reset the grabbed object's properties
        grabbedObject.GetComponent<PickableObject>().objectGrabbed = false;

        Destroy(gameObject);
    }

    // Stop all motion of the magnet, disables the Circle Collider 2D and destroys the magnet
    void StopAllMotion()
    {
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        circleCollider.enabled = false;
        rb.freezeRotation = true;

        anim.Play("magnetBreakAnim");
        Destroy(gameObject, breakAnimLength);
    }
}
