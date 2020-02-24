using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    bool firstText = false, secondText = false;
    [SerializeField] GameObject buttonsHud;
    // Update is called once per frame
    void Start()
    {
        Time.timeScale = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!firstText) {
                firstText = true;
                GameObject.Find("TutorialCanvas").SetActive(false);
                buttonsHud.SetActive(true);
                Time.timeScale = 1;
            } 
        }
    }
}
