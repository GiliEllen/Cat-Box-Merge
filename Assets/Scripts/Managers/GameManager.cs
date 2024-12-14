using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Dictionary<int, bool> levelProgress = new Dictionary<int, bool>();
    public bool isGameOver = false;
    public int levelToLoad;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadProgress();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            RebindButtons();
        }
    }

    private void RebindButtons()
    {
        Button startButton = GameObject.Find("StartButton").GetComponent<Button>();
        // Button exitButton = GameObject.Find("ExitButton").GetComponent<Button>();

        if (startButton != null)
        {
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(() => startButtonMethod());
        }

        // if (exitButton != null)
        // {
        //     exitButton.onClick.RemoveAllListeners();
        //     exitButton.onClick.AddListener(() => ExitButton());
        // }
    }

    public void CompleteLevel(int levelIndex)
    {
        levelProgress[levelIndex] = true;
        SaveProgress();
        SceneManager.LoadScene("MainMenu");
    }

    private void SaveProgress()
    {
        // Serialize and save data (e.g., PlayerPrefs, JSON).
    }

    private void LoadProgress()
    {
        // Deserialize and load data.
    }

    public void LoadLevel(int levelIndex)
    {
        string LevelName = "Level " + levelIndex;
        SceneManager.LoadScene(LevelName);
    }

    public void startButtonMethod()
    {
        Debug.Log("test");
        int levelIndex = levelProgress.Count + 1;
        Debug.Log(levelProgress.Count);
        LoadLevel(levelIndex);
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
