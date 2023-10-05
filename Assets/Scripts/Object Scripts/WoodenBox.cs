using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBox : MonoBehaviour
{
    public Animator breakingAnim;
    public GameObject metalParticle;
    private PickableObject pickableObject;

    void Awake()
    {
        pickableObject = GetComponent<PickableObject>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Danger")
        {
            for (int i = 0; i < Random.Range(5, 20); i++)
            {
                GameObject particle = Instantiate(metalParticle, transform.position, Quaternion.identity);
                particle.GetComponent<BoxCollider2D>().enabled = false;

                particle.GetComponent<Rigidbody2D>().AddForce
                (
                    new Vector2(Random.Range(-10, 10),
                    Random.Range(-10, 10)), ForceMode2D.Impulse
                );

                Destroy(particle, 2f);
            }
            pickableObject.enabled = false;
            breakingAnim.SetBool("Broken", true);
        }
    }
}
