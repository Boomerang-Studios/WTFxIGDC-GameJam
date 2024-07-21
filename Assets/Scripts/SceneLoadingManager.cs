using UnityEngine;

public class SceneLoadingManager : Singleton<SceneLoadingManager>
{
    public int CurrentSceneIndex => UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
    public int GetSceneCount => UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;

    public void LoadNextScene()
    {
        int nextSceneIndex = CurrentSceneIndex + 1;
        if (nextSceneIndex < GetSceneCount)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No more scenes to load.");
        }
    }

    public void LoadPreviousScene()
    {
        int previousSceneIndex = CurrentSceneIndex - 1;
        if (previousSceneIndex >= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(previousSceneIndex);
        }
        else
        {
            Debug.LogWarning("No more scenes to load.");
        }
    }

    public void ReloadCurrentScene()
    {
        LoadScene(CurrentSceneIndex);
    }

    public void LoadScene(int a_Index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(a_Index);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
        Application.OpenURL("about:blank");
#else
        Application.Quit();
#endif
    }
}
