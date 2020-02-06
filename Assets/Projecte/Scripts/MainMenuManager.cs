using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
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
        SceneManager.LoadScene("2D");
    }
}
