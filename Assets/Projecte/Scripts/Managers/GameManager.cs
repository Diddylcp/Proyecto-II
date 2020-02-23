using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindWithTag("EnemyTower") == null || GameObject.FindWithTag("AllyTower") == null)
        {
            SceneManager.LoadScene(0);
        }
    }
}
