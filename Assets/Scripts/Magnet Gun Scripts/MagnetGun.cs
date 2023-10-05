using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

/*

    THIS SCRIPT USES UNITY'S NEW INPUT SYSTEM


    To make the Magnet Gun work properly, you need to create the following Game Objects and Tags in Unity

    Player (Parent) --- Tag = "Player"
        MagnetGun (child of Player) --- Tag = "MagnetGun"
            FirePoint (child of MagnetGun) --- Tag = any

        Attach this script (MagnetGun.cs) and
            Player Input to the MagnetGun object

        Add an event where you will reference the Shoot method in the Player Input
    
    Assign the FirePoint object to the firePoint Transform variable
    Assign the Magnet Prefab object to the magnetPrefab GameObject variable

    If you decide to animate the gun, you can create a new child gameObject
        with Sprite Renderer and the Animator to the FirePoint GameObject

    Assign the layers to whatIsObject LayerMask variable, where the objects you want to pick up with the magnet, are
        (See instructions in PickableObject.cs for creating a moveable object)


    THE LINES OF CODE MARKED WITH A COMMENT AT THE END, CAN BE DELETED WITHOUT IT AFFECTING PERFORMANCE OF THE GUN

*/

public class MagnetGun : MonoBehaviour
{
    // Public variables visible in the Unity Inspector
    [Header("Gun properties")]
    public float firePower;
    public float magnetReturnSpeed = 10f;

    [Header("Other")]
    public float returnTimer;
    [SerializeField] private float detectionRadius = 0.4f;

    [Header("References")]
    public GameObject magnetPrefab;
    public Transform magnetSprite;
    public Transform firePoint;
    public Animator pullGunAnim;                                                                /* ANIMATION IS NOT NECESSARY */
    public LayerMask whatIsObject;
    public ThrowSFX throwSFX;

    // Hidden variable accessible from other scripts
    [HideInInspector] public bool hasBeenShot = false;

    // Private variables only accessible within this script
    private bool isTouchingAnObject = false;
    private Camera mainCamera;
    private Vector2 mousePosition;
    private GameObject magnet;
    private PauseMenu pauseMenu;                                                                /**/

    void Awake()
    {
        // Find references to other scripts and components when the object is created

        pauseMenu = FindAnyObjectByType<PauseMenu>();                                           /**/
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Check if the game is not paused
        if (pauseMenu.isGamePaused == false)                                                    /**/
        {                                                                                       /**/
            // Check if the gun is touching an object within the detection radius
            isTouchingAnObject = Physics2D.OverlapCircle(
                new Vector2(firePoint.position.x - 0.125f, firePoint.position.y), detectionRadius, whatIsObject);

            // Get the current mouse position
            mousePosition = Mouse.current.position.ReadValue();

            // Convert mouse position to world coordinates
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(
                new Vector3(mousePosition.x, mousePosition.y, mainCamera.nearClipPlane));

            // Calculate the direction from the gun to the mouse position
            Vector3 direction = mouseWorldPosition - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotate the gun to point towards the mouse position
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (mouseWorldPosition.x < GameObject.FindWithTag("Player").transform.position.x)
            {
                magnetSprite.localScale = new Vector3(0.5f, -0.5f, 0.5f);
            }
            else
            {
                magnetSprite.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }

            // Call the animation function
            AnimatePullGun();                                                                   /**/
        }                                                                                       /**/
    }

    // Called when the Shoot action is triggered
    public void Shoot(InputAction.CallbackContext context)
    {
        // Check if the game is not paused and the gun is not touching an object
        if (pauseMenu.isGamePaused == false && isTouchingAnObject == false)                     /* PAUSEMENU CAN BE DELETED */
        {
            // Check if the shoot action was performed and the gun hasn't been shot yet
            if (context.performed && hasBeenShot == false)
            {
                throwSFX.PlayThrowSound();

                // Instantiate a magnet prefab and apply a force to it
                magnet = Instantiate(magnetPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D magnetRB = magnet.GetComponent<Rigidbody2D>();
                magnetRB.AddForce(firePoint.right * firePower, ForceMode2D.Impulse);

                // Mark the gun as shot
                hasBeenShot = true;
            }
        }
    }

    // Handle the gun animation
    void AnimatePullGun()                                                                       /* IF YOU DON'T WANT TO ANIMATE THE GUN, YOU CAN DELETE THIS METHOD*/
    {
        if (hasBeenShot == true)
        {
            // Trigger the released animation when the gun has been shot
            pullGunAnim.SetTrigger("Released");
        }
        else
        {
            // Trigger the hide gun animation when the gun hasn't been shot
            pullGunAnim.SetTrigger("HideGun");
        }

        // Check if an object is being grabbed by the magnet and update the animation accordingly
        if (magnet != null && magnet.GetComponent<Magnet>().grabbed == true)
        {
            pullGunAnim.SetBool("IsGrabbing", true);
        }
        else
        {
            pullGunAnim.SetBool("IsGrabbing", false);
        }
    }

    // Draw a visual representation of the detection radius in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(firePoint.position.x, firePoint.position.y), detectionRadius);
    }
}