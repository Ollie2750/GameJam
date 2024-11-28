using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    
    public void PlayButton()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void ExitButton()
    {
        EditorApplication.ExitPlaymode();
        Application.Quit();
    }

    public void TryAgainButton()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
