using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float xPos;
    private float yPos;
    void Update()
    {
        xPos = transform.position.x;
        yPos = transform.position.y;
    }
    public IEnumerator Shake(float duration)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(xPos - .01f, xPos + .01f);
            float y = Random.Range(yPos - .01f, yPos + .01f);

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
