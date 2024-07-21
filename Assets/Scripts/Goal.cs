using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] bool m_LoadNextScene;
    [SerializeField] int m_SceneIndexToLoad;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (m_LoadNextScene)
            {
                SceneLoadingManager.Instance.LoadNextScene();
            }
            else
            {
                SceneLoadingManager.Instance.LoadScene(m_SceneIndexToLoad);
            }
        }
    }
}
