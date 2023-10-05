using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerCheck : MonoBehaviour
{
    public GameObject musicManagerPrefab;
    void Start()
    {
        if (FindAnyObjectByType<MusicManager>() == false)
        {
            Instantiate(musicManagerPrefab);
        }
    }
}
