using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool pause = false;
    [SerializeField] private GameObject buttonsHUD, pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Menu()
    {
        Time.timeScale = 1;
        pause = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pause = false;
        buttonsHUD.SetActive(true);
        pauseMenu.SetActive(false);
    }

    void Pause()
    {
        Time.timeScale = 0;
        pause = true;
        buttonsHUD.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
