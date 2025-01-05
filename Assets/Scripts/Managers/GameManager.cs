using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Dictionary<int, bool> levelProgress = new Dictionary<int, bool>();
    public bool isGameOver = false;
    public int levelToLoad;

     public int finalLevel; 
    public GameObject gameWinPanel;
    public GameObject mainMenuPanel;
    public GameObject loadingPanel;

    private void Awake()
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

    public void ActivateLoader() {
        Debug.Log("work bitch");
        loadingPanel.SetActive(true);
    }
    public void DeActivateLoader() {
        loadingPanel.SetActive(false);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ActivateLoader();
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
        DeActivateLoader();
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
        ActivateLoader();
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
        DeActivateLoader();
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

    public void LoadLevel(int levelIndex)
    {
        string LevelName = "Level " + levelIndex;
        mainMenuPanel.SetActive(false);
        gameWinPanel.SetActive(false);
        SceneManager.LoadScene(LevelName);
    }

    public async void startButtonMethod()
    {
        ActivateLoader();
        int levelIndex = levelProgress.Count + 1;
        Debug.Log(levelProgress.Count);
        await Task.Delay(2000);
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
