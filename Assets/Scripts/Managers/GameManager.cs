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

     public int finalLevel; 
    public GameObject gameWinPanel;
    public GameObject mainMenuPanel;

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

    if (gameWinPanel != null)
    {
        Button restartButton = gameWinPanel.transform.Find("RestartButton").GetComponent<Button>();
        if (restartButton != null)
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(RestartGame);
        }
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
        Debug.Log("here2");
        levelProgress[levelIndex] = true;
        SceneManager.LoadScene("MainMenu");

        if (levelIndex == finalLevel)
        {
            Debug.Log("win?");
            ShowGameWinPanel();
            mainMenuPanel.SetActive(false);
        } else {
            mainMenuPanel.SetActive(true);
        }
    }


    private void ShowGameWinPanel()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false);
        } else {
            Debug.Log("still null");
        }
        if (gameWinPanel != null)
        {
            gameWinPanel.SetActive(true);
        } else {
            Debug.Log("still null");
        }
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
        mainMenuPanel.SetActive(false);
        gameWinPanel.SetActive(false);
        SceneManager.LoadScene(LevelName);
    }

    public void startButtonMethod()
    {
        int levelIndex = levelProgress.Count + 1;
        Debug.Log(levelProgress.Count);
        LoadLevel(levelIndex);
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        levelProgress.Clear();
        isGameOver = false;

        SceneManager.LoadScene("MainMenu");
        Destroy(gameObject); 
    }
}
