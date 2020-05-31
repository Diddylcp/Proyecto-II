using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level01");
    }

    public void Controles()
    {
        SceneManager.LoadScene("Controles");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToTutorialMap()
    {
        SceneManager.LoadScene("Level01");
    }

    public void LevelSelection()
    {
        SceneManager.LoadScene("Level Selection");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
