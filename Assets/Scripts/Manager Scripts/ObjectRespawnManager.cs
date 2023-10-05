using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRespawnManager : MonoBehaviour
{
    public GameObject[] objectsInRoom;
    public Transform[] startingPoints;
    public GameObject objectPrefab;
    public float respawnTimer = 1f;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "MoveableObject" || collider.gameObject.tag == "LightBox")
        {
            GameObject fallenObject = collider.gameObject;

            Debug.Log("Object hit");

            for (int i = 0; i < objectsInRoom.Length; i++)
            {
                if (fallenObject == objectsInRoom[i])
                {
                    fallenObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    fallenObject.transform.position = startingPoints[i].position;
                    fallenObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
                }
            }
        }
    }

    public void RespawnObjects()
    {
        Invoke("RespawnTime", respawnTimer);
    }

    void RespawnTime()
    {
        for (int i = 0; i < objectsInRoom.Length; i++)
        {
            if (objectPrefab.GetComponentInChildren<BomBox>())
            {
                Instantiate(objectPrefab, startingPoints[i].position, Quaternion.identity);
                break;
            }

            if (objectsInRoom[i] != null)
            {
                objectsInRoom[i].transform.position = startingPoints[i].position;
                objectsInRoom[i].transform.rotation = new Quaternion(0, 0, 0, 0);
            }

        }
    }
}
