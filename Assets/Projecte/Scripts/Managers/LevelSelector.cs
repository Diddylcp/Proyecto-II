using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    // Start is called before the first frame update
    public void Tutorial()
    {
        SceneManager.LoadScene("level01");
    }

    public void Level02()
    {
        SceneManager.LoadScene("Level02");
    }

    public void Level03()
    {
        SceneManager.LoadScene("Level03");
    }

    public void Level04()
    {
        SceneManager.LoadScene("Level04");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
