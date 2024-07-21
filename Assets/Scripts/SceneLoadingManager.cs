using UnityEngine;

public class SceneLoadingManager : Singleton<SceneLoadingManager>
{
    public int CurrentSceneIndex => UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
    public int GetSceneCount => UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void LoadNextScene()
    {
        int nextSceneIndex = CurrentSceneIndex + 1;
        if (nextSceneIndex < GetSceneCount)
        {
            SoundManager.Instance.PlayMusic();
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
            SoundManager.Instance.PlayMusic();
            UnityEngine.SceneManagement.SceneManager.LoadScene(previousSceneIndex);
        }
        else
        {
            Debug.LogWarning("No more scenes to load.");
        }
    }

    public void ReloadCurrentScene()
    {
        SoundManager.Instance.PlayMusic();
        LoadScene(CurrentSceneIndex);
    }

    public void LoadScene(int a_Index)
    {
        SoundManager.Instance.PlayMusic();
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
