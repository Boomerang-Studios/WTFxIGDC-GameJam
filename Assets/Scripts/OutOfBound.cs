using System.Collections;
using UnityEngine;

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
        SceneLoadingManager.Instance.ReloadCurrentScene();
        //SceneManager.LoadScene(SceneManager.GetActiveScene());
    }
}
