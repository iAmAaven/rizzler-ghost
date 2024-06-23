using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BomBoxSpawner : MonoBehaviour
{
    [Header("BomBox")]
    public GameObject bomBoxPrefab;
    public float spriteTime;

    [Header("Gizmos")]
    public float detectionWidth;
    public float detectionHeight;

    [Header("References")]
    public LayerMask playerLayer;
    public Transform boxSpawnPoint;
    public Animator buttonAnim;

    private bool isInRange = false;
    private bool buttonOutlineOn = false;
    [SerializeField] private bool toggleOldInput = false;
    private PauseMenu pauseMenu;

    void Start()
    {
        pauseMenu = FindAnyObjectByType<PauseMenu>();
    }
    void Update()
    {
        isInRange = Physics2D.OverlapBox(transform.position,
            new Vector2(detectionWidth, detectionHeight), 1, playerLayer);

        if (isInRange && buttonOutlineOn == false)
        {
            buttonAnim.SetTrigger("InRange");
            buttonOutlineOn = true;
        }
        else if (isInRange == false && buttonOutlineOn)
        {
            buttonAnim.SetTrigger("OutOfRange");
            buttonOutlineOn = false;
        }

        if (!pauseMenu.isGamePaused && toggleOldInput && Input.GetButtonDown("OpenDoor")
            && isInRange && FindAnyObjectByType<BomBox>() == false)
        {
            SpawnBomBox();
        }
    }
    public void ButtonPress(InputAction.CallbackContext context)
    {
        if (!pauseMenu.isGamePaused && context.performed
            && isInRange && FindAnyObjectByType<BomBox>() == false && !toggleOldInput)
        {
            SpawnBomBox();
        }
    }

    void SpawnBomBox()
    {
        GameObject newBomBox = Instantiate(bomBoxPrefab, boxSpawnPoint.position, Quaternion.identity);
        newBomBox.GetComponentInChildren<BomBox>().timeBetweenEachSprite = spriteTime;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position,
            new Vector3(detectionWidth, detectionHeight, 0));
    }
}
