using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Start is called before the first frame update

    public static CameraShake instance;
    Vector3 originalPos;
    private float magnitudeVal = 0.1f;
    public float duration;
    Coroutine coroutine;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            originalPos = transform.localPosition;
        }
        else
        {
            Destroy(this);
        }

    }

    public void Shake()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        coroutine = StartCoroutine(ShakeCoroutine());

    }

    IEnumerator ShakeCoroutine()
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitudeVal;
            float y = Random.Range(-1f, 1f) * magnitudeVal;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}