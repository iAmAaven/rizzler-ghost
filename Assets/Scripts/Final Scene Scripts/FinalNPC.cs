using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalNPC : MonoBehaviour
{
    public float timerToMainMenu;
    public Animator anim;
    public GameObject[] possibleGhosts;
    private GameObject ghost;
    private int randomChance;
    private int ghostNumber;

    void Start()
    {
        randomChance = Random.Range(1, 101);

        if (randomChance < 40)
        {
            ghost = possibleGhosts[0];
            ghostNumber = 0;
        }
        else if (randomChance >= 40 && randomChance <= 70)
        {
            ghost = possibleGhosts[1];
            ghostNumber = 1;
        }
        else
        {
            ghost = possibleGhosts[2];
            ghostNumber = 2;
        }

        ghost.SetActive(true);
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            anim.SetTrigger("Ghost" + ghostNumber);
            collider.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collider.gameObject.GetComponent<PlayerMovement>().enabled = false;
            collider.gameObject.GetComponent<PlayerMovement>().isGrounded = false;
            Destroy(collider.gameObject, 1.5f);

            Invoke("SwitchScene", timerToMainMenu);
        }
    }

    void SwitchScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
