using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBound : MonoBehaviour
{
    WaitForSeconds delay = new WaitForSeconds(1);
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(SceneReload());
        }
    }

    IEnumerator SceneReload()
    {
        yield return delay;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
