using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }

    private int currentScene;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            LoadPrevScene();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            LoadNextScene();
        }
    }

    public void LoadNextScene()
    {
        if (currentScene >= SceneManager.sceneCountInBuildSettings || currentScene < 0) return;

        int nextScene = ++currentScene;
        SceneManager.LoadScene(nextScene);
    }

    public void LoadPrevScene()
    {
        Debug.Log(SceneManager.sceneCountInBuildSettings);
        if (currentScene > SceneManager.sceneCountInBuildSettings || currentScene <= 0) return;

        int nextScene = --currentScene;
        SceneManager.LoadScene(nextScene);
    }
}
