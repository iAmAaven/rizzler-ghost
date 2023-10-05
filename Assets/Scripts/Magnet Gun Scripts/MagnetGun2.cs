using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MagnetGun2 : MonoBehaviour
{
    [Header("Gun properties")]
    public float firePower;
    public float magnetLifeTimer = 5f;
    public float magnetReturnSpeed = 10f;

    [Header("Other")]
    public float returnTimer;
    [SerializeField] private float detectionRadius = 0.75f;


    [Header("References")]
    public GameObject magnet2Prefab;
    public Transform firePoint;
    public Transform targetPos;
    public Animator magnetGunAnim;
    public LayerMask whatIsObject;
    public Transform magnetSprite;

    [HideInInspector] public bool hasBeenShot = false;

    // PRIVATES     

    private Camera mainCamera;
    private Vector2 mousePosition;
    private PauseMenu pauseMenu;
    private bool isTouchingAnObject = false;

    void Awake()
    {
        pauseMenu = FindAnyObjectByType<PauseMenu>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (pauseMenu.isGamePaused == false)
        {
            isTouchingAnObject = Physics2D.OverlapCircle(firePoint.position, detectionRadius, whatIsObject);

            mousePosition = Mouse.current.position.ReadValue();
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.nearClipPlane));
            Vector3 direction = mouseWorldPosition - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (mouseWorldPosition.x < GameObject.FindWithTag("Player").transform.position.x)
            {
                magnetSprite.localScale = new Vector3(0.5f, -0.5f, 0.5f);
            }
            else
            {
                magnetSprite.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }

            AnimatePullGun();
        }
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        if (pauseMenu.isGamePaused == false && isTouchingAnObject == false)
        {
            if (context.performed && hasBeenShot == false)
            {
                hasBeenShot = true;
                Instantiate(magnet2Prefab, firePoint.position, Quaternion.identity);
            }
        }
    }

    void AnimatePullGun()
    {
        if (hasBeenShot == true)
        {
            magnetGunAnim.SetTrigger("Released");
        }
        else
        {
            magnetGunAnim.SetTrigger("HideGun");
        }
    }
}
