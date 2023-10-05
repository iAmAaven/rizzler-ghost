using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomBox : MonoBehaviour
{
    [Header("Timers")]
    public float destructionTimer = 2f;
    public float timeBetweenEachSprite;

    [Header("Properties")]
    public float explosionPower = 30;
    public int minParticleAmount = 30;
    public int maxParticleAmount = 50;

    [Header("References")]
    public SpriteRenderer spriteRenderer;
    public Sprite[] boxSprites;
    public GameObject[] boxPieces;
    public AudioSource explosionSFX;
    public GameObject bomBoxPrefab;


    // PRIVATES

    private Animator explosionAnim;
    private Transform parentPos;
    private Vector3 boxStartPos;

    List<GameObject> boxParts = new List<GameObject>();


    void Start()
    {
        parentPos = GetComponentInParent<Transform>();
        boxStartPos = new Vector3(parentPos.position.x, parentPos.position.y, parentPos.position.z);
        StartCoroutine(Countdown());
        explosionAnim = GetComponent<Animator>();
    }

    void Update()
    {
        explosionSFX.volume = PlayerPrefs.GetFloat("sfxVolume");
    }

    void Explode()
    {
        // instantiate box parts to fly off to random direction
        explosionSFX.Play();
        explosionAnim.Play("bomBoxExplosion");
        Invoke("ExplosionOnTimer", 0.5f);
    }

    IEnumerator Countdown()
    {
        for (int i = 0; i < boxSprites.Length; i++)
        {
            spriteRenderer.sprite = boxSprites[i];
            yield return new WaitForSeconds(timeBetweenEachSprite);
        }
        Explode();
    }
    void ExplosionOnTimer()
    {
        spriteRenderer.sprite = null;
        GetComponent<BoxCollider2D>().enabled = false;

        for (int i = 0; i < Random.Range(minParticleAmount, maxParticleAmount); i++)
        {
            GameObject boxPiece = Instantiate(boxPieces[Random.Range(0, boxPieces.Length - 1)], transform.position, Quaternion.identity);
            boxPiece.GetComponent<Rigidbody2D>().AddForce
            (
                new Vector2(Random.Range(-explosionPower, explosionPower),
                Random.Range(-explosionPower, explosionPower)), ForceMode2D.Impulse
            );
            boxParts.Add(boxPiece);

            Destroy(boxPiece, 2f);
        }

        GameObject player = GameObject.FindWithTag("Player");
        Destroy(player);

        // foreach (GameObject boxParticle in boxParts)
        // {
        //     Destroy(boxParticle, 2);
        // }
        // Debug.Log(boxParts[2]);
        // boxParts.Clear();

        FindAnyObjectByType<DeathManager>().PlayerDied(); // CALLS THE PlayerDied METHOD FROM DEATHMANAGER
        FindAnyObjectByType<RespawnManager>().RespawnPlayer(); // CALLS THE RespawnPlayer METHOD FROM RESPAWNMANAGER

        if (FindAnyObjectByType<ObjectRespawnManager>() == true)
        {
            FindAnyObjectByType<ObjectRespawnManager>().RespawnObjects();
        }

        Destroy(parentPos.gameObject, 1);
    }

    void RespawnBox()
    {
        if (FindAnyObjectByType<ObjectRespawnManager>() == true)
            FindAnyObjectByType<ObjectRespawnManager>().RespawnObjects();
    }
}
