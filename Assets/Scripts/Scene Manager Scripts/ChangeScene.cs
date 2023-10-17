using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneToLoad;
    public void SceneChange()
    {
        Invoke("SceneTimer", 1.5f);
    }

    void SceneTimer()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
