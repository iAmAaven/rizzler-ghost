using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BomBoxRespawner : MonoBehaviour
{
    [Header("Properties")]
    public float pushForce = 15f;
    public float spawnWaitTime = 2f;
    public float setBomBoxTimer = 3;
    [Header("Gizmos")]
    public float detectionWidth;
    public float detectionHeight;

    [Header("References")]
    public LayerMask playerLayer;
    public GameObject bomBoxPrefab;
    public Transform respawnPoint;
    public Transform inRangeArea;
    public Animator buttonAnim;
    public Animator doorAnim;

    private bool isInRange = false;
    private bool buttonPressed = false;

    void Update()
    {
        isInRange = Physics2D.OverlapBox(inRangeArea.position,
            new Vector2(detectionWidth, detectionHeight), 1, playerLayer);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(inRangeArea.position,
            new Vector3(detectionWidth, detectionHeight, 0));
    }

    public void PressRedButton(InputAction.CallbackContext context)
    {
        if (context.performed && isInRange == true && buttonPressed == false)
        {
            buttonAnim.SetTrigger("Interact");
            doorAnim.SetTrigger("Open");
            if (FindAnyObjectByType<BomBox>() == false)
            {
                buttonPressed = true;
                Invoke("SpawnBomBox", spawnWaitTime);
            }
        }
    }
    void SpawnBomBox()
    {
        GameObject newBomBox = Instantiate(bomBoxPrefab, respawnPoint.position, Quaternion.identity);

        newBomBox.GetComponentInChildren<BomBox>().timeBetweenEachSprite = setBomBoxTimer;
        Rigidbody2D bomBoxRB = newBomBox.GetComponentInChildren<Rigidbody2D>();

        bomBoxRB.AddForce(Vector2.up * pushForce * bomBoxRB.mass, ForceMode2D.Impulse);

        Invoke("WaitToPressButton", 1f);
    }

    void WaitToPressButton()
    {
        buttonPressed = false;
    }
}