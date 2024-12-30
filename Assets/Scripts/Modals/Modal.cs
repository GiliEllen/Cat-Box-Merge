using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class Modal : MonoBehaviour
{
    public void restartLevel() {
        Scene currentScene = SceneManager.GetActiveScene ();
		string sceneName = currentScene.name;
        SceneManager.LoadScene(sceneName);
    }

    public void GoBackToMainMenu() {
        Debug.Log("test");
        SceneManager.LoadScene("MainMenu");
    }
}