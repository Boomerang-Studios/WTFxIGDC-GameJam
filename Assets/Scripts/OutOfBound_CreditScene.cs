using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBound_CreditScene : MonoBehaviour
{
    WaitForSeconds delay = new WaitForSeconds(1);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(LoadScene());
        }
    }
    
    IEnumerator LoadScene()
    {
        yield return delay;
        SceneManager.LoadScene(0);
    }
}
