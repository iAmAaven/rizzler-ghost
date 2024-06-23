using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement settings")]
    public float playerSpeed = 5;
    public float jumpForce = 5;

    [Header("Other")]
    public LayerMask whatIsGround;
    public Transform feet;
    public float groundCheckRadius = 0.2f;
    public AudioSource jumpSFX;

    // PRIVATES

    private float horizontalMovement;
    [HideInInspector] public bool isGrounded;
    private PauseMenu pauseMenu;
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private bool toggleOldInput = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        pauseMenu = FindAnyObjectByType<PauseMenu>();

        PickableObject[] objects = GameObject.FindObjectsOfType<PickableObject>();
        foreach (PickableObject pickable in objects)
        {
            pickable.RefreshMagnetGun();
        }
    }
    void Update()
    {
        if (pauseMenu.isGamePaused == false)
        {
            isGrounded = Physics2D.OverlapCircle(feet.position, groundCheckRadius, whatIsGround);

            if (toggleOldInput == true)
            {
                horizontalMovement = Input.GetAxisRaw("Horizontal");

                if (Input.GetButtonDown("Jump") && isGrounded)
                {
                    anim.SetTrigger("Jump");
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                }
                else if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                }
            }
            AnimateCharacter();
        }
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMovement * playerSpeed, rb.velocity.y);
    }

    void AnimateCharacter()
    {
        if (horizontalMovement != 0)
        {
            anim.SetBool("IsWalking", true);
            if (isGrounded == false)
            {
                anim.SetBool("IsWalking", false);
            }
            else
            {
                anim.SetBool("IsWalking", true);
            }
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }

        if (horizontalMovement < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (horizontalMovement > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (toggleOldInput == false)
        {
            horizontalMovement = context.ReadValue<Vector2>().x;
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (pauseMenu.isGamePaused == false && toggleOldInput == false)
        {
            if (context.performed && isGrounded)
            {
                jumpSFX.Play();
            }
            if (context.started && isGrounded)
            {
                anim.SetTrigger("Jump");
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (context.canceled && rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(feet.position, groundCheckRadius);
    }
}